using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.ViewModels;
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

        [HttpPost]
        public async Task<IActionResult> GetProductCategories()
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
    }
}
