
using Acr.Assist.AuditTrail.Core.DTO;
using Acr.Assist.AuditTrail.Core.Exception;
using Acr.Assist.AuditTrail.Core.Services;
using Acr.Assist.AuditTrail.Service.Validator;
using ACR.Assist.AuditTrail.Core.Data;
using ACR.Assist.AuditTrail.Core.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acr.Assist.AuditTrail.Service
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly IAuditTrailRepository audittrailRepository;
        private readonly IDataValidator<AddAuditTrailEntry> addaudittrailEntryValidator;
        private readonly IMapper mapper;

        public AuditTrailService(
            IAuditTrailRepository audittrailRepository,
            IDataValidator<AddAuditTrailEntry> addaudittrailEntryValidator,
            IMapper mapper)
        {
            this.audittrailRepository = audittrailRepository;
            this.addaudittrailEntryValidator = addaudittrailEntryValidator;
            this.mapper = mapper;
        }

        public async Task<AuditTrailEntry> AddAuditTrail (AddAuditTrailEntry addaudittrailentry)
        {
            var validatorResult = addaudittrailEntryValidator.ValidateInstance(addaudittrailentry);
            if (!validatorResult.IsValid)
            {
                throw new InputValidationFailureException(validatorResult.Errors);
            }

            var auditTrailEntry = mapper.Map<AuditTrailEntry>(addaudittrailentry);

            auditTrailEntry.LogID = Guid.NewGuid();
            auditTrailEntry.Username = addaudittrailentry.Username;
            auditTrailEntry.ModuleName = addaudittrailentry.ModuleName;
            auditTrailEntry.ActionType = addaudittrailentry.ActionType;
            auditTrailEntry.Description = addaudittrailentry.Description;
            auditTrailEntry.Detailed_Description = addaudittrailentry.Detailed_Description;
            auditTrailEntry.LoggedTime = DateTime.UtcNow;

            auditTrailEntry.LogID = await audittrailRepository.AddAuditTrail(auditTrailEntry);
            return auditTrailEntry;
        }

    }
}
