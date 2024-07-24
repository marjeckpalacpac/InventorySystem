using Inventory.DataAccess.Data;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public class PurchaseRequestService : IPurchaseRequestService
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRequestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<PurchaseRequest> products, int recordsTotal, int recordsFiltered)> SearchPurchaseRequest(DataTableParams tableParams)
        {
            var query = _dbContext.PurchaseRequests.AsNoTracking();

            int recordsTotal = await query.CountAsync();
            int recordsFiltered = recordsTotal;

            List<PurchaseRequest> products;
            if (!string.IsNullOrEmpty(tableParams.SearchValue))
            {
                string searchValue = tableParams.SearchValue.ToLower();

                query = query.Where(w =>
                    w.Id.ToString().Contains(searchValue)
                    || w.PRNumber.ToLower().Contains(searchValue));

                recordsFiltered = await query.CountAsync();
            }

            products = await query.Skip(tableParams.Start).Take(tableParams.Length).ToListAsync();

            return (products, recordsTotal, recordsFiltered);
        }
    }
}
