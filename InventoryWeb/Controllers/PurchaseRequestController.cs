using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private readonly IPurchaseRequestService _purchaseRequest;
        private readonly IMapper _mapper;
        public PurchaseRequestController(IPurchaseRequestService purchaseRequest, IMapper mapper)
        {
            _purchaseRequest = purchaseRequest;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls
        [HttpPost]
        public async Task<IActionResult> SearchPurchaseRequest()
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

            var (products, recordsTotal, recordsFiltered) = await _purchaseRequest.SearchPurchaseRequest(dataTableParams);

            var productVM = _mapper.Map<List<ProductViewModel>>(products);

            return Json(new { recordsTotal, recordsFiltered, data = productVM });
        }
        #endregion
    }
}
