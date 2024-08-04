using Inventory.Models.Common;
using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class PurchaseRequestViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string? PRNumber { get; set; }
        public string? Description { get; set; }
        public List<PurchaseRequestDetailViewModel> PurchaseRequestDetails { get; set; } = new();

    }
}
