using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public int MinimumStock { get; set; }
        public string Unit { get; set; }
    }
}
