using AutoMapper;
using Inventory.DataAccess.Services;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _company;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService company, IMapper mapper)
        {
            _company = company;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }


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
