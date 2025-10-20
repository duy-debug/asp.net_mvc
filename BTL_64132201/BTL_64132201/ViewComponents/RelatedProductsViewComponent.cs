using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;
using BTL_64132201.DAL;

namespace BTL_64132201.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        ProductDAL productDAL = new ProductDAL();
        public IViewComponentResult Invoke(int id)
        {
            List<Product> list = new List<Product>();
            list = productDAL.GetRelatedProducts(id);
            return View("RelatedProduct", list);
        }
    }
}
