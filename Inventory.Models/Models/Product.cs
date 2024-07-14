using Inventory.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Models
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public ProductCategory? ProductCategory { get; set; }

        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        public int MinimumStock { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
    }
}
