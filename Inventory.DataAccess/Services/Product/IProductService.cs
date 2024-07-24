using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public interface IProductService
    {
        Task CreateProduct(Product info);
        Task<bool> DeleteProduct(int id);
        Task<Product?> GetProduct(int id);
        Task<bool> IsNameExist(int id, string name);
        Task<(List<Product> products, int recordsTotal, int recordsFiltered)> SearchProduct(DataTableParams tableParams);
        Task<bool> UpdateProduct(Product info);
    }
}
