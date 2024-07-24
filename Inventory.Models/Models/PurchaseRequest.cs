using Inventory.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Models
{
    public class PurchaseRequest : AuditableEntity
    {
        public int Id { get; set; }
        public string PRNumber { get; set; }
        public string? Description { get; set; }

        public List<PurchaseRequestDetail> PurchaseRequestDetails { get; set; } = new();
        public void SetPRNumber()
        {
            PRNumber = $"PR{Id:D9}";
        }
    }
}
