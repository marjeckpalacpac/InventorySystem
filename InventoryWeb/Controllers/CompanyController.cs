using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Inventory.Utility.Helpers;
using Inventory.Utility.Misc;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _company;
        private readonly IMapper _mapper;
        private readonly ILookupListingService _lookupListingService;

        public CompanyController(ICompanyService company, IMapper mapper, ILookupListingService lookupListingService)
        {
            _company = company;
            _mapper = mapper;
            _lookupListingService = lookupListingService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CompanyViewModel vm = new();
            await PopulateCommonList(vm);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _company.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View(vm); 
                }

                Company info = _mapper.Map<Company>(vm);

                await _company.CreateCompany(info);
                TempData[TempDataNotification.Success] = string.Concat("Company ", info.Name, " created successfully");

                return View(nameof(Index));
            }
            await PopulateCommonList(vm);

            TempData[TempDataNotification.Error] = "Something went wrong";
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var company = await _company.GetCompany((int)id);

            if (company == null)
                return NotFound();

            var vm = _mapper.Map<CompanyViewModel>(company);
            await PopulateCommonList(vm);
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var isNameExist = await _company.IsNameExist(vm.Id, vm.Name);
                if (isNameExist)
                {
                    //Custom validation
                    ModelState.AddModelError("Name", "Name is already exist!");
                    return View();
                }

                Company info = _mapper.Map<Company>(vm);

                bool isUpdated = await _company.UpdateCompany(info);
                if (isUpdated)
                {
                    TempData[TempDataNotification.Success] = string.Concat("Company ", info.Name, " updated successfully");
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

            var company = await _company.GetCompany((int)id);

            if (company == null)
                return NotFound();

            var vm = _mapper.Map<CompanyViewModel>(company);
            await PopulateCommonList(vm);

            return View(vm);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            bool isUpdated = await _company.DeleteCompany(id);
            if (isUpdated)
            {
                TempData[TempDataNotification.Success] = "Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
                return NotFound();

        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {

            var company = await _company.GetCompany((int)id);

            if (company == null)
                return NotFound();

            var vm = _mapper.Map<CompanyViewModel>(company);
            await PopulateCommonList(vm);

            return View(vm);

        }

        #region Non http requests
        private async Task PopulateCommonList(CompanyViewModel vm)
        {
            var supplyChainPartners = await _lookupListingService.SupplyChainPartners();
            vm.SupplyChainPartnerSelectList = supplyChainPartners.ToSelectListItems();
        }
        #endregion

        #region API Calls
        [HttpPost]
        public async Task<IActionResult> SearchCompany()
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

            var (companies, recordsTotal, recordsFiltered) = await _company.SearchCompany(dataTableParams);

            var companyVM = _mapper.Map<List<CompanyViewModel>>(companies);

            return Json(new { recordsTotal, recordsFiltered, data = companyVM });
        }
        #endregion
    }
}
