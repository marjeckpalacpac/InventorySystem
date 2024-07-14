
using Inventory.Models.CustomValidations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        public string? TelephoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public bool SystemOwner { get; set; }

        [MinLengthList(1, ErrorMessage = "At least one item required in Supply Chain Partner")]
        public List<string>? SupplyChainPartners { get; set; }
        public IEnumerable<SelectListItem> SupplyChainPartnerSelectList { get; set; } = new List<SelectListItem>();
    }
}
