using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class PurchaseRequestDetailViewModel
    {
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }
        [ValidateNever]
        public ProductViewModel? Product { get; set; }
        public double Quantity { get; set; }
        
    }
}
