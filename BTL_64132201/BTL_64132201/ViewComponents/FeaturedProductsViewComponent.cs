using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        ProductDAL productDAL = new ProductDAL();
        public IViewComponentResult Invoke(int? limit)
        {
            int limitProduct = limit ?? 4;
            List<Product> list = new List<Product>();
            list = productDAL.FeaturedProducts(limitProduct);
            return View("FeaturedProduct", list);
        }
    }
}
