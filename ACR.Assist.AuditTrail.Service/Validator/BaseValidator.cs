using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acr.Assist.AuditTrail.Service.Validator
{
    public abstract class BaseValidator<T> : AbstractValidator<T>, IDataValidator<T> where T : class
    {
        public ValidatorResult ValidateInstance(T instance)
        {
            ValidatorResult validatorResult = new ValidatorResult();
            var results = Validate(instance);

            validatorResult.IsValid = results.IsValid;
            if (!validatorResult.IsValid)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    validatorResult.Errors.Add(failure.PropertyName, failure.ErrorMessage);
                }
            }
            return validatorResult;
        }
    }
}
