using System.Diagnostics;
using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Controllers
{
    public class HomeController : Controller
    {
        ProductDAL productDAL = new ProductDAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? idCategory, int? idAuthor)
        {
            List<Product> list = new List<Product>();
            list = productDAL.GetListProducts(idCategory, idAuthor);
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
