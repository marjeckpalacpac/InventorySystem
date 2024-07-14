using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Services
{
    public interface ILookupListingService
    {
        Task<List<LookupListing>> SupplyChainPartners(bool? isActiveOnly = true);
    }
}
