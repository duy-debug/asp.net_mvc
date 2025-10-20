using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
