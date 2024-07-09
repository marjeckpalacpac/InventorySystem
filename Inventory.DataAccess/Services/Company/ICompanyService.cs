using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public interface ICompanyService
    {
        Task<(List<Company> companies, int recordsTotal, int recordsFiltered)> SearchCompany(DataTableParams tableParams);
    }
}
