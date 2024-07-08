using Inventory.DataAccess.Data;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<(List<ProductCategory> productCategories, int recordsTotal, int recordsFiltered)> SearchProductCategory(DataTableParams tableParams)
        {
            var query = _dbContext.ProductCategories.Where(w => w.IsActive);

            int recordsTotal = await query.CountAsync();
            int recordsFiltered = recordsTotal;

            List<ProductCategory> productCategories;
            if (!string.IsNullOrEmpty(tableParams.SearchValue))
            {
               string searchValue = tableParams.SearchValue.ToLower();

                query = query.Where(w =>
                    w.Id.ToString().Contains(searchValue)
                    || w.Name.ToLower().Contains(searchValue));

                recordsFiltered = await query.CountAsync();
            }

            productCategories = await query.Skip(tableParams.Start).Take(tableParams.Length).ToListAsync();

            return (productCategories, recordsTotal, recordsFiltered);
        }

        public async Task<ProductCategory?> GetProductCategory(int id)
        {
            var record = await _dbContext.ProductCategories.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id && w.IsActive);

            return record;
        }

        public async Task CreateProductCategory(ProductCategory info)
        {
            await _dbContext.ProductCategories.AddAsync(info);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductCategory(ProductCategory info)
        {
            var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(w => w.Id == info.Id && w.IsActive);
            if (productCategory == null)
                return false;

            productCategory.Name = info.Name;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductCategory(int id)
        {
            var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(w => w.Id == id && w.IsActive);
            if (productCategory == null)
                return false;

            productCategory.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
