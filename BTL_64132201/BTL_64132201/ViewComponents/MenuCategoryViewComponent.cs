using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        CategoryDAL categoryDAL = new CategoryDAL();
        public IViewComponentResult Invoke()
        {
            List<CategoryMenu> list = new List<CategoryMenu>();
            list = categoryDAL.getCategoryWithCount();
            return View("Default", list);
        }
    }
}
