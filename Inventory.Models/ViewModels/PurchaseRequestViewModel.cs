using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class PurchaseRequestViewModel
    {
        public int Id { get; set; }
        public string PRNumber { get; set; }
        public string? Description { get; set; }
    }
}
