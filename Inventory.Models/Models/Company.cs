using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? TelephoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public bool SystemOwner { get; set; } = false;
        public bool IsActive { get; set; } = true;

        [NotMapped]
        public List<string> SupplyChainPartners { get; set; } = new();
    }
}
