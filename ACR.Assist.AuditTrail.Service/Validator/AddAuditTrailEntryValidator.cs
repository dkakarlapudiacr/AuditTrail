using System;
using System.Collections.Generic;
using System.Text;
using Acr.Assist.AuditTrail.Service.DataValidation;
using ACR.Assist.AuditTrail.Core.DTO;
using FluentValidation;
namespace Acr.Assist.AuditTrail.Service.Validator
{
    public class AddAuditTrailEntryValidator : BaseValidator<AddAuditTrailEntry>
    {
        public AddAuditTrailEntryValidator()
        {
            RuleFor(p => p.Username).NotEmpty().WithMessage(ExceptionMessages.UsernameEmpty);
            RuleFor(p => p.ModuleName).NotEmpty().WithMessage(ExceptionMessages.ModuleNameEmpty);
            RuleFor(p => p.ActionType).NotEmpty().WithMessage(ExceptionMessages.ActionTypeEmpty);
            RuleFor(p => p.Description).NotEmpty().WithMessage(ExceptionMessages.DescriptionEmpty);
            RuleFor(p => p.Detailed_Description).NotEmpty().WithMessage(ExceptionMessages.DetailedDescriptionEmpty);
        }
    }
}

