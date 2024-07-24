using Inventory.DataAccess.Data;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var query = from p in _dbContext.Products
                        where p.IsActive
                        join pc in _dbContext.ProductCategories.Where(pc => pc.IsActive) on p.ProductCategoryId equals pc.Id into pcGroup
                        from pc in pcGroup.DefaultIfEmpty()
                        join s in _dbContext.Suppliers.Where(s => s.IsActive) on p.SupplierId equals s.Id into sGroup
                        from s in sGroup.DefaultIfEmpty()
                        join c in _dbContext.Companies.Where(c => c.IsActive) on s.CompanyId equals c.Id into cGroup
                        from c in cGroup.DefaultIfEmpty()
                        join uom in _dbContext.UnitOfMeasurements.Where(uom => uom.IsActive) on p.UnitOfMeasurementId equals uom.Id into uomGroup
                        from uom in uomGroup.DefaultIfEmpty()
                        select new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            MinimumStock = p.MinimumStock,
                            ProductCategory = pc ?? new ProductCategory(),
                            UnitOfMeasurement = uom ?? new UnitOfMeasurement(),
                            Supplier  = new Supplier() { Company = c ?? new() },
                        };

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

        public async Task<Product?> GetProduct(int id)
        {
            var record = await _dbContext.Products
                .Include(pc => pc.ProductCategory)
                .Include(s => s.Supplier)
                .Include(uom => uom.UnitOfMeasurement)
                .AsNoTracking().FirstOrDefaultAsync(f => f.Id == id && f.IsActive);

            return record;
        }
        public async Task CreateProduct(Product info)
        {
            await _dbContext.Products.AddAsync(info);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(Product info)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(w => w.Id == info.Id && w.IsActive);
            if (product == null)
                return false;

            product.Name = info.Name;
            product.MinimumStock = info.MinimumStock;
            product.SupplierId = info.SupplierId;
            product.ProductCategoryId = info.ProductCategoryId;
            product.UnitOfMeasurementId = info.UnitOfMeasurementId;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(w => w.Id == id && w.IsActive);
            if (product == null)
                return false;

            product.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsNameExist(int id, string name)
        {
            var exist = await _dbContext.Products
                .AnyAsync(w => w.Name.ToLower() == name.ToLower()
                && w.Id != id && w.IsActive);

            return exist;
        }
    }
}
