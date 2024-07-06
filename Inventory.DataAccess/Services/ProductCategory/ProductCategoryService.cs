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
            var productCategories = await _dbContext.ProductCategories.ToListAsync();
            
            int recordsTotal = productCategories.Count();
            int recordsFiltered = recordsTotal;

            if (!string.IsNullOrEmpty(tableParams.SearchValue))
            {
                string searchValue = tableParams.SearchValue.ToLower();
                productCategories = productCategories.Where(w =>
                    w.Id.ToString().Contains(searchValue)
                    || w.Name.ToLower().Contains(searchValue)).ToList();

                recordsFiltered = productCategories.Count();
            }

            productCategories = productCategories.Skip(tableParams.Start).Take(tableParams.Length).ToList();

            return (productCategories, recordsTotal, recordsFiltered);
        }
    }
}
