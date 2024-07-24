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
        Task<List<LookupListing>> ProductCategoryListing(bool? isActiveOnly = true);
        Task<List<LookupListing>> SupplierListing(bool? isActiveOnly = true);
        Task<List<LookupListing>> SupplyChainPartnersListing(bool? isActiveOnly = true);
        Task<List<LookupListing>> UnitOfMeasurementListing(bool? isActiveOnly = true);
    }
}
