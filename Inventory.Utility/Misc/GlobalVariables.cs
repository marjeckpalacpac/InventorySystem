using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Utility.Misc
{
    public static class SupplyChainPartners
    {
        public static string Supplier { get; } = "Supplier";
        public static string Customer { get; } = "Customer";
    }

    public static class TempDataNotification
    {
        public static string Success { get; } = "success";
        public static string Error { get; } = "error";
    }
}
