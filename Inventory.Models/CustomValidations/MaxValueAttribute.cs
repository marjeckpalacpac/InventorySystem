using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.CustomValidations
{
    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public MaxValueAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && int.TryParse(value.ToString(), out int intValue))
            {
                if (intValue > _maxValue)
                {
                    return new ValidationResult(ErrorMessage ?? $"The value must not exceed {_maxValue}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
