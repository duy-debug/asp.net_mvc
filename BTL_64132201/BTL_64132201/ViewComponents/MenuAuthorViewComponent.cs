using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.ViewComponents
{
    public class MenuAuthorViewComponent : ViewComponent
    {
        AuthorDAL authorDAL = new AuthorDAL();
        public IViewComponentResult Invoke()
        {
            List<AuthorMenu> list = new List<AuthorMenu>();
            list = authorDAL.getAuthorWithCount();
            return View("Default", list);
        }
    }
}
