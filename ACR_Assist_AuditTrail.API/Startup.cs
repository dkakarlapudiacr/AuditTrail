using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Acr.Assist.AuditTrail.Infrastructure;
using Acr.Assist.AuditTrail.Core.Infrastructure.Configuration;
using Acr.Assist.AuditTrail.Core.Integrations;
using Acr.Assist.AuditTrail.Data;
using Acr.Assist.AuditTrail.Service.Validator;
using ACR.Assist.AuditTrail.Integrations;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using ACR.Assist.AuditTrail.Core.Data;
using ACR.Assist.AuditTrail.Core.DTO;
using Acr.Assist.AuditTrail.Service;

namespace Acr.Assist.AuditTrail
{
    public class Startup
    {
        /// <summary>
        /// Program starts here
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the hosting environment.
        /// </summary>
        public IWebHostEnvironment HostingEnvironment { get; }


        /// <summary>
        /// The swagger schema name
        /// </summary>
        private readonly string swaggerSchemaName = "Bearer";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Data.Startup.Configure();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            //Configure logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var authConfig = Configuration.GetSection("AuthorizationConfig").Get<AuthorizationConfig>();
            var connectionString = Configuration["MongoConnection:ConnectionString"];
            var mongoDBName = Configuration["MongoConnection:DataBase"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authConfig.Issuer,
                        ValidAudience = authConfig.Audience,
                        IssuerSigningKey = GetKey(authConfig.KeyFilePath)
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserIdExists",
                    policy => policy.Requirements.Add(new UserIdRequirement("UserId")));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMvc();

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(GetMimeTypesForCompression());
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.SetIsOriginAllowed((host) => true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddTransient<Core.Services.IAuditTrailService, AuditTrailService>();
            services.AddTransient<IAuditTrailRepository>(s => new AuditTrailRepository(connectionString, mongoDBName));
            services.AddSingleton<IAuthorizationHandler, UserIdExistsRequirementHandler>();
            services.AddTransient<IAuthorizationMicroService, AuthorizationMicroService>();
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddSingleton<AuthorizationConfig>(authConfig);
            services.AddTransient<IDataValidator<AddAuditTrailEntry>, AddAuditTrailEntryValidator>();

            services.AddSingleton<IWebHostEnvironment>(HostingEnvironment);
            services.AddSingleton<Serilog.ILogger>(Log.Logger);
            //services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Version"], new OpenApiInfo { Title = Configuration["Title"], Version = Configuration["Version"] });
                c.AddSecurityDefinition(swaggerSchemaName, GetSwaggerSecurityScheme());
                c.OperationFilter<SecurityRequirementsOperationFilter>(swaggerSchemaName);
            });
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvcCore().AddApiExplorer();

            services.AddControllers();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCors("AllowAllOrigins");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.RoutePrefix = Configuration["Environment:SwaggerRoutePrefix"];
                c.DocumentTitle = Configuration["Title"] + " " + Configuration["Version"];
                c.SwaggerEndpoint(Configuration["Environment:BaseURL"] + "/swagger/" + Configuration["Version"] + "/swagger.json", Configuration["Title"] + " " + Configuration["Version"]);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="keyFilePath">The key file path.</param>
        /// <returns></returns>
        private X509SecurityKey GetKey(string keyFilePath)
        {
            X509Certificate2 certificate;
            var certificatePath = HostingEnvironment.WebRootPath + keyFilePath;
            certificate = new X509Certificate2(certificatePath);
            return new X509SecurityKey(certificate);
        }

        /// <summary>
        /// Gets the swagger security scheme.
        /// </summary>
        /// <returns></returns>
        private OpenApiSecurityScheme GetSwaggerSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header. Example: " + "{token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT"
            };
        }

        /// <summary>
        /// Gets the MIME types for compression.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetMimeTypesForCompression()
        {
            return new[]
            {
                "application/json",
                "image/png",
                "image/jpeg",
                "image/gif",
                "image/tiff",
                "image/webp"
            };
        }
    }
}