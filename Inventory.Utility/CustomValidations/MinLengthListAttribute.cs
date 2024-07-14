using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Utility.CustomValidations
{
    public class MinLengthListAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinLengthListAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;
            if (list != null && list.Count >= _minLength)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"At least {_minLength} items are required.");
        }
    }
}
