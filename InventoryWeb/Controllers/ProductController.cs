using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Helpers;
using Inventory.Utility.Misc;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        private readonly ILookupListingService _lookupListing;

        public ProductController(IProductService product, IMapper mapper, ILookupListingService _lookupListing)
        {
            _product = product;
            _mapper = mapper;
            this._lookupListing = _lookupListing;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductViewModel vm = new();
            await PopulateCommonList(vm);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _product.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View(vm);
                }

                Product info = _mapper.Map<Product>(vm);

                await _product.CreateProduct(info);
                TempData[TempDataNotification.Success] = string.Concat("Company ", info.Name, " created successfully");

                return View(nameof(Index));
            }

            await PopulateCommonList(vm);

            TempData[TempDataNotification.Error] = "Something went wrong";
            return View(vm);
        }

        #region Non http requests
        private async Task PopulateCommonList(ProductViewModel vm)
        {
            var productCategories = await _lookupListing.ProductCategoryListing();
            vm.ProductCategorySelectList = productCategories.ToSelectListItems();

            var suppliers = await _lookupListing.SupplierListing();
            vm.SupplierSelectList = suppliers.ToSelectListItems();

            var unitOfMeasurements = await _lookupListing.UnitOfMeasurementListing();
            vm.UnitOfMeasurementSelectList = unitOfMeasurements.ToSelectListItems();
        }
        #endregion

        #region API Calls
        [HttpPost]
        public async Task<IActionResult> SearchProduct()
        {
            string? draw = Request.Form["draw"].FirstOrDefault();

            var dataTableParams = new DataTableParams
            {
                Start = Convert.ToInt32((Request.Form["start"].FirstOrDefault() ?? "0")),
                Length = Convert.ToInt32((Request.Form["length"].FirstOrDefault() ?? "0")),
                SearchValue = Request.Form["search[value]"].FirstOrDefault(),
                SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault(),
                SortDirection = Request.Form["order[0][dir]"].FirstOrDefault()
            };

            var (products, recordsTotal, recordsFiltered) = await _product.SearchProduct(dataTableParams);

            var productVM = _mapper.Map<List<ProductViewModel>>(products);

            return Json(new { recordsTotal, recordsFiltered, data = productVM });
        }
        #endregion
    }
}
