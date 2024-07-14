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
        public async Task<List<LookupListing>> SupplyChainPartners(bool? isActiveOnly = true)
        {
            List<LookupListing> lookupListings = await _dbContext.LookupListings
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
    }
}
