using DataAnnotationsExtensions;
using Inventory.Models.Common;
using Inventory.Models.CustomValidations;
using Inventory.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "At least one item required in Product Category")]
        public int? ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        [MinValue(1)]
        public int MinimumStock { get; set; }

        [Required(ErrorMessage = "At least one item required in Unit of Measurement")]
        public int? UnitOfMeasurementId { get; set; }
        public UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public IEnumerable<SelectListItem> ProductCategorySelectList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> SupplierSelectList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> UnitOfMeasurementSelectList { get; set; } = new List<SelectListItem>();
    }
}
