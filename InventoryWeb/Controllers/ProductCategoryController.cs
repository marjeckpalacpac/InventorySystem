using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Misc;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategory;
        private readonly IMapper _mapper;

        public ProductCategoryController(IProductCategoryService productCategory, IMapper mapper)
        {
            _productCategory = productCategory;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _productCategory.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    //Custom validation
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View(vm);
                }

                ProductCategory info = _mapper.Map<ProductCategory>(vm);

                await _productCategory.CreateProductCategory(info);
                TempData[TempDataNotification.Success] = string.Concat("Product category ", info.Name, " created successfully");

                return View(nameof(Index));
            }

            TempData[TempDataNotification.Error] = "Something went wrong";
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var productCategory = await _productCategory.GetProductCategory((int)id);

            if (productCategory == null)
                return NotFound();

            var vm = _mapper.Map<ProductCategoryViewModel>(productCategory);

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _productCategory.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    //Custom validation
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View();
                }

                ProductCategory info = _mapper.Map<ProductCategory>(vm);

                bool isUpdated = await _productCategory.UpdateProductCategory(info);
                if (isUpdated)
                {
                    TempData[TempDataNotification.Success] = string.Concat("Product category ", info.Name, " updated successfully");
                    return RedirectToAction(nameof(Index)); 
                }
                else
                    return NotFound();
            }

            TempData[TempDataNotification.Error] = "Something went wrong";
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var productCategory = await _productCategory.GetProductCategory((int)id);

            if (productCategory == null)
                return NotFound();

            var vm = _mapper.Map<ProductCategoryViewModel>(productCategory);

            return View(vm);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            bool isUpdated = await _productCategory.DeleteProductCategory(id);
            if (isUpdated)
            {
                TempData[TempDataNotification.Success] = "Deleted successfully";
                return RedirectToAction(nameof(Index)); 
            }
            else
                return NotFound();

        }

        #region API Calls
        [HttpPost]
        public async Task<IActionResult> SearchProductCategory()
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

            var (productCategories, recordsTotal, recordsFiltered) = await _productCategory.SearchProductCategory(dataTableParams);

            var productCategoriesVM = _mapper.Map<List<ProductCategoryViewModel>>(productCategories);

            return Json(new { recordsTotal, recordsFiltered, data = productCategoriesVM });
        }
        #endregion

    }
}
