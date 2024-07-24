using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public interface IPurchaseRequestService
    {
        Task<(List<PurchaseRequest> products, int recordsTotal, int recordsFiltered)> SearchPurchaseRequest(DataTableParams tableParams);
    }
}
