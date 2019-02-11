using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using RegistrationForm.Infrastructure.Security;

namespace RegistrationForm.Infrastructure.Validators
{
    public class SecurePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordChecker = validationContext.GetService<IPasswordComplexityChecker>();

            if(value is string password && passwordChecker!= null && passwordChecker.Check(password))
                return ValidationResult.Success;
            return new ValidationResult(ErrorMessage);
        }
    }
}