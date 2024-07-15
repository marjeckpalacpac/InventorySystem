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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Product> products, int recordsTotal, int recordsFiltered)> SearchProduct(DataTableParams tableParams)
        {
            var query = _dbContext.Products.Include(pc => pc.ProductCategory).Where(w => w.IsActive);

            int recordsTotal = await query.CountAsync();
            int recordsFiltered = recordsTotal;

            List<Product> products;
            if (!string.IsNullOrEmpty(tableParams.SearchValue))
            {
                string searchValue = tableParams.SearchValue.ToLower();

                query = query.Where(w =>
                    w.Id.ToString().Contains(searchValue)
                    || w.Name.ToLower().Contains(searchValue)
                    || w.ProductCategory.Name.ToLower().Contains(searchValue));

                recordsFiltered = await query.CountAsync();
            }

            products = await query.Skip(tableParams.Start).Take(tableParams.Length).ToListAsync();

            return (products, recordsTotal, recordsFiltered);
        }
    }
}
