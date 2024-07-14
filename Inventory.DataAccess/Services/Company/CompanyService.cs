using Inventory.DataAccess.Data;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Misc;
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
        public async Task<Company?> GetCompany(int id)
        {
            var record = await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id && f.IsActive);

            if (record != null)
            {
                var supplyChainPartner = await _dbContext.Suppliers.AsNoTracking()
                    .Select(s => new { s.CompanyId, SupplyChainPartner = SupplyChainPartners.Supplier, s.IsActive })
                    .FirstOrDefaultAsync(f => f.CompanyId == record.Id && f.IsActive);

                if (supplyChainPartner != null)
                    record.SupplyChainPartners.Add(supplyChainPartner.SupplyChainPartner);

                supplyChainPartner = await _dbContext.Customers.AsNoTracking().
                    Select(s => new { s.CompanyId, SupplyChainPartner = SupplyChainPartners.Customer, s.IsActive }).
                    FirstOrDefaultAsync(f => f.CompanyId == record.Id && f.IsActive);
                if (supplyChainPartner != null)
                    record.SupplyChainPartners.Add(supplyChainPartner.SupplyChainPartner);
            }

            return record;
        }
        public async Task CreateCompany(Company info)
        {
            await _dbContext.Companies.AddAsync(info);

            if (info.SupplyChainPartners.First(f => f == SupplyChainPartners.Supplier).Any())
            {
                var supplier = new Supplier()
                {
                    Company = info
                };

                await _dbContext.Suppliers.AddAsync(supplier);
            }

            if (info.SupplyChainPartners.First(f => f == SupplyChainPartners.Customer).Any())
            {
                var customer = new Customer()
                {
                    Company = info
                };

                await _dbContext.Customers.AddAsync(customer);
            }


            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateCompany(Company info)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(w => w.Id == info.Id && w.IsActive);
            if (company == null)
                return false;

            company.Name = info.Name;
            company.Address = info.Address;
            company.TelephoneNo = info.TelephoneNo;
            company.MobileNo = info.MobileNo;
            company.ContactPerson = info.ContactPerson;
            company.Email = info.Email;

            if (info.SupplyChainPartners.FirstOrDefault(f => f == SupplyChainPartners.Supplier) != null)
            {
                var asSupplier = await _dbContext.Suppliers.AsNoTracking().FirstOrDefaultAsync(f => f.CompanyId == info.Id && f.IsActive);
                if (asSupplier == null)
                {
                    var supplier = new Supplier()
                    {
                        Company = company
                    };

                    await _dbContext.Suppliers.AddAsync(supplier);
                }
            }
            else
            {
                var asSupplier = await _dbContext.Suppliers.FirstOrDefaultAsync(f => f.CompanyId == info.Id && f.IsActive);
                if (asSupplier != null)
                {
                    asSupplier.IsActive = false;
                }
            }

            if (info.SupplyChainPartners.FirstOrDefault(f => f == SupplyChainPartners.Customer) != null)
            {
                var asCustomer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(f => f.CompanyId == info.Id && f.IsActive);
                if (asCustomer == null)
                {
                    var customer = new Customer()
                    {
                        Company = company
                    };

                    await _dbContext.Customers.AddAsync(customer);
                }
            }
            else
            {
                var asCustomer = await _dbContext.Customers.FirstOrDefaultAsync(f => f.CompanyId == info.Id && f.IsActive);
                if (asCustomer != null)
                {
                    asCustomer.IsActive = false;
                }
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(w => w.Id == id && w.IsActive);
            if (company == null)
                return false;

            company.IsActive = false;

            var asSupplier = await _dbContext.Suppliers.FirstOrDefaultAsync(f => f.CompanyId == id && f.IsActive);
            if (asSupplier != null)
                asSupplier.IsActive = false;

            var asCustomer = await _dbContext.Customers.FirstOrDefaultAsync(f => f.CompanyId == id && f.IsActive);
            if (asCustomer != null)
                asCustomer.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsNameExist(int id, string name)
        {
            var exist = await _dbContext.Companies
                .AnyAsync(w => w.Name.ToLower() == name.ToLower()
                && w.Id != id && w.IsActive);

            return exist;
        }


    }
}
