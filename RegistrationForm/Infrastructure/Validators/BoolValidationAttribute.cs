using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationForm.Infrastructure.Validators
{
    public class BoolValidationAttribute : ValidationAttribute
    {
        private readonly bool _value;

        public BoolValidationAttribute(bool value)
        {
            _value = value;
        }

        public override bool IsValid(object value)
        {
            return value is bool boolValue && boolValue == _value;
        }
    }
}
