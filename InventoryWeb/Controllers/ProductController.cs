using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Helpers;
using Inventory.Utility.Misc;
using Microsoft.AspNetCore.Http.HttpResults;
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
            vm.FormAction = FormAction.Create;
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

            //TempData[TempDataNotification.Error] = "Something went wrong";
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _product.GetProduct(id);

            if (product == null)
                return View("NotFound", new NotFoundViewModel() { Message = $"The product with id of {id} could not be found." });

            var vm = _mapper.Map<ProductViewModel>(product);
            vm.FormAction = FormAction.Edit;
            await PopulateCommonList(vm);
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _product.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    //Custom validation
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View();
                }

                Product info = _mapper.Map<Product>(vm);

                bool isUpdated = await _product.UpdateProduct(info);
                if (isUpdated)
                {
                    TempData[TempDataNotification.Success] = string.Concat("Product ", info.Name, " updated successfully");
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View("NotFound", new NotFoundViewModel() { Message = $"The product with id of {vm.Id} could not be found." });
            }

            //TempData[TempDataNotification.Error] = "Something went wrong";
            await PopulateCommonList(vm);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _product.GetProduct(id);

            if (product == null)
                return View("NotFound", new NotFoundViewModel() { Message = $"The product with id of {id} could not be found." });

            var vm = _mapper.Map<ProductViewModel>(product);
            vm.FormAction = FormAction.Delete;
            await PopulateCommonList(vm);

            return View(vm);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            //No need for validation here. The Model will be validated if the parameter of the action method (DeletePost) is also a Model. From current case it is int id
            bool isUpdated = await _product.DeleteProduct(id);
            if (isUpdated)
            {
                TempData[TempDataNotification.Success] = "Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
                return View("NotFound", new NotFoundViewModel() { Message = $"The product with id of {id} could not be found." });

        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            var product = await _product.GetProduct(id);

            if (product == null)
                return View("NotFound", new NotFoundViewModel() { Message = $"The product with id of {id} could not be found." });

            var vm = _mapper.Map<ProductViewModel>(product);
            vm.FormAction = FormAction.Detail;
            await PopulateCommonList(vm);

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
