using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.CustomValidations
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && int.TryParse(value.ToString(), out int intValue))
            {
                if (intValue < _minValue)
                {
                    return new ValidationResult(ErrorMessage ?? $"The value must be at least {_minValue}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
