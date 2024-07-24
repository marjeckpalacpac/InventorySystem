using Inventory.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Inventory.DataAccess.Services
{
    public class LookupListingService : ILookupListingService
    {
        private readonly ApplicationDbContext _dbContext;

        public LookupListingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<LookupListing>> SupplyChainPartnersListing(bool? isActiveOnly = true)
        {
            List<LookupListing> lookupListings = await _dbContext.LookupListings.AsNoTracking()
                .Where(
                    w => w.Code == LookupListingCode.SupplyChainPartner.ToString() &&
                    w.IsActive == isActiveOnly
                    ).ToListAsync();
            return lookupListings;
        }

        public enum LookupListingCode
        {
            SupplyChainPartner,
        }

        //From Tables
        public async Task<List<LookupListing>> ProductCategoryListing(bool? isActiveOnly = true)
        {
            List<LookupListing> lookupListings = await _dbContext.ProductCategories.AsNoTracking()
                .Where(w => w.IsActive == isActiveOnly)
                .Select(s => new LookupListing() { Name = s.Name, Id = s.Id})
                .ToListAsync();
            return lookupListings;
        }
        public async Task<List<LookupListing>> SupplierListing(bool? isActiveOnly = true)
        {
            List<LookupListing> lookupListings = await _dbContext.Suppliers.Include(s => s.Company).AsNoTracking()
                .Where(w => w.IsActive == isActiveOnly && w.Company.IsActive == isActiveOnly)
                .Select(s => new LookupListing() { Name = s.Company.Name, Id = s.Id })
                .ToListAsync();
            return lookupListings;
        }

        public async Task<List<LookupListing>> UnitOfMeasurementListing(bool? isActiveOnly = true)
        {
            List<LookupListing> lookupListings = await _dbContext.UnitOfMeasurements.AsNoTracking()
                .Where(w => w.IsActive == isActiveOnly)
                .Select(s => new LookupListing() { Name = s.Name, Id = s.Id })
                .ToListAsync();
            return lookupListings;
        }
    }
}
