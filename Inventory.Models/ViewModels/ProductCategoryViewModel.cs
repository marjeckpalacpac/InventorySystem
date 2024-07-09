using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "Maximum length of character is 50")]
        public string Name { get; set; }
    }
}
