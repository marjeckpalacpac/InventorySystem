using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;

        public ProductController(IProductService product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }


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
