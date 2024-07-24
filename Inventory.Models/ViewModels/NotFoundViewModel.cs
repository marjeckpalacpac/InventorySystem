using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.ViewModels
{
    public class NotFoundViewModel
    {
        public bool ItemNotFound { get; set; } = true;
        public string Message { get; set; }
    }
}
