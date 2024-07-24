using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Common
{
    public class BaseViewModel
    {
        [ValidateNever]
        public string FormAction { get; set; }
    }
}
