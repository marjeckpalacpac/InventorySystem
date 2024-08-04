using AutoMapper;
using Inventory.DataAccess.Data;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Misc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryWeb.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private readonly IPurchaseRequestService _purchaseRequest;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRequestController(IPurchaseRequestService purchaseRequest, IMapper mapper, ApplicationDbContext dbContext)
        {
            _purchaseRequest = purchaseRequest;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PurchaseRequestViewModel vm = new();
            vm.FormAction = FormAction.Create;

            //await PopulateCommonList(vm);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PurchaseRequestViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            PurchaseRequestViewModel vm = new();
            vm.FormAction = FormAction.Edit;
            vm.Description = "test Desc";
            vm.PurchaseRequestDetails.Add(new PurchaseRequestDetailViewModel() { Product = new ProductViewModel() { Id = 2, Name = "Coke" } });
            vm.PurchaseRequestDetails.Add(new PurchaseRequestDetailViewModel() { Product = new ProductViewModel() { Id = 3, Name = "Tiles" } });
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(PurchaseRequestViewModel model)
        {
            return View(model);
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

        [HttpGet]
        public JsonResult SearchProduct([FromQuery]  string term, [FromQuery] string except)
        {
            var exceptList = JsonConvert.DeserializeObject<List<string>>(except);

            var results = _dbContext.Products
                .Where(w => w.Name.ToLower().Contains(term.ToLower()) && !exceptList.Contains(w.Name))
                .Take(2)
                .ToList();

            return Json(results);
        }
        #endregion
    }
}
