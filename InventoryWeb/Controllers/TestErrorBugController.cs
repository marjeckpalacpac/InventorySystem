using Microsoft.AspNetCore.Mvc;

namespace InventoryWeb.Controllers
{
    public class TestErrorBugController : Controller
    {
        public IActionResult Index()
        {
            int one = 1;
            int zero = 0;
            int ans = one / zero;

            return View();
        }
    }
}
