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
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Company> companies, int recordsTotal, int recordsFiltered)> SearchCompany(DataTableParams tableParams)
        {
            var query = _dbContext.Companies.Where(w => w.IsActive);

            int recordsTotal = await query.CountAsync();
            int recordsFiltered = recordsTotal;

            List<Company> companies;
            if (!string.IsNullOrEmpty(tableParams.SearchValue))
            {
               string searchValue = tableParams.SearchValue.ToLower();

                query = query.Where(w =>
                    w.Id.ToString().Contains(searchValue)
                    || w.Name.ToLower().Contains(searchValue));

                recordsFiltered = await query.CountAsync();
            }

            companies = await query.Skip(tableParams.Start).Take(tableParams.Length).ToListAsync();

            return (companies, recordsTotal, recordsFiltered);
        }

        
    }
}
