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
        Task CreateCompany(Company info);
        Task<bool> DeleteCompany(int id);
        Task<Company?> GetCompany(int id);
        Task<bool> IsNameExist(int id, string name);
        Task<(List<Company> companies, int recordsTotal, int recordsFiltered)> SearchCompany(DataTableParams tableParams);
        Task<bool> UpdateCompany(Company info);
    }
}
