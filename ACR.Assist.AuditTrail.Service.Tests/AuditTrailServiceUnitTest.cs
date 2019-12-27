using Acr.Assist.AuditTrail.Data;
using Acr.Assist.AuditTrail.Service.Validator;
using System.Collections.Generic;
using Acr.Assist.AuditTrail.Core.Infrastructure.Configuration;
using Serilog;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Acr.Assist.AuditTrail.Core.Profile;
using Acr.Assist.AuditTrail.Service;
using Acr.Assist.AuditTrail.Service.Tests;
using Acr.Assist.AuditTrail.Infrastructure;
using Xunit;
using ACR.Assist.AuditTrail.Core.DTO;
using System;
using Acr.Assist.AuditTrail.Core.Exception;
using System.Threading.Tasks;

namespace ACR.Assist.AuditTrail.Service.Tests
{
    public class AuditTrailServiceUnitTest
    {
        /// <summary>
        /// The audittrail repository
        /// </summary>
        private readonly AuditTrailRepository audittrailRepository;

        /// <summary>
        /// The add audittrail validator
        /// </summary>
        private readonly AddAuditTrailEntryValidator addaudittrailValidator;


        /// <summary>
        /// The sut
        /// </summary>
        private readonly AuditTrailService sut;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigurationManager configurationManager;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;


        /// <summary>
        /// The add audittrail entries
        /// </summary>
        public static IEnumerable<object[]> AddAuditTrailEntries = UnitTestData.GetAddAuditTrailEntries();

        public AuditTrailServiceUnitTest()
        {
        audittrailRepository = new AuditTrailRepository(Constants.ConnectionString, Constants.DBName);
        addaudittrailValidator = new AddAuditTrailEntryValidator();

        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();
        configurationManager = new ConfigurationManager(configuration);
        logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        var AuditTrailEntryProfile = new AuditTrailEntryProfiles();


        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(AuditTrailEntryProfile);
        });

        mapper = new Mapper(mapperConfig);

        sut = new AuditTrailService(audittrailRepository, addaudittrailValidator, mapper);
    }
        /// <summary>
        /// Adds audit trail input validation failure.
        /// </summary>
        /// <param name="entries">The entries.</param>
        [Theory]
        [MemberData(nameof(AddAuditTrailEntries))]
        public async Task AddAuditTrailInputValidationFailure(List<AddAuditTrailEntry> entries)
        {
            foreach (var entry in entries)
            {
                Exception ex = await Assert.ThrowsAsync<InputValidationFailureException>(() => sut.AddAuditTrail(entry));
            }
        }
    }
}
