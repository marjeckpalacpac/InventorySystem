using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Models
{
    public class LookupListing
    {
        [MaxLength(100)]
        public string? Code { get; set; }

        [MaxLength(110)]
        public string? Name { get; set; }
        public int? Id { get; set; }

        [MaxLength(50)]
        public string? Value { get; set; }
        public int? SortId { get; set; }
        public bool IsActive { get; set; }
    }
}
