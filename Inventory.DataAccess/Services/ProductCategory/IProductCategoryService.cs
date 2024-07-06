using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public interface IProductCategoryService
    {
        Task<(List<ProductCategory> productCategories, int recordsTotal, int recordsFiltered)> SearchProductCategory(DataTableParams tableParams);
    }
}
