using Inventory.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Models
{
    public class UnitOfMeasurement : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PackagingType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
