using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL productDAL = new ProductDAL();
        public IActionResult Index(int? idCategory, int? idAuthor, int page = 1, string sortOrder = "")
        {
            int pageSize = 6;
            ViewData["IdCategory"] = idCategory;
            ViewData["IdAuthor"] = idAuthor;
            ViewData["SortColumn"] = sortOrder;
            List<Product> products = new List<Product>();
            products = productDAL.GetProducts_Pagination(idCategory, idAuthor, page, pageSize, sortOrder);
            int rowCount = productDAL.GetListProducts(idCategory, idAuthor).Count();
            double pageCount = (double)rowCount / pageSize;
            int maxPage = (int)Math.Ceiling(pageCount);
            ProductPagination model = new ProductPagination();
            model.Products = products;
            model.CurrentPageIndex = page;
            model.PageCount = maxPage;
            return View(model);
        }
        public IActionResult Detail(int id)
        {
            Product product = new Product();
            product = productDAL.GetProductById(id);
            return View(product);
        }
    }
}