using BTL_64132201.DAL;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_64132201.ViewComponents
{
    public class MenuDynamicViewComponent : ViewComponent
    {
        private readonly MenuDAL menuDAL = new MenuDAL();

        public IViewComponentResult Invoke()
        {
            string RoleCustomer = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            List<MenuItem> listMenu = menuDAL.GetAllMenu().Where(m => m.isVisible).ToList();
            var itemsToRemove = new List<MenuItem>();
            foreach (var item in listMenu)
            {
                if ((RoleCustomer != "Administrator" && item.MenuUrl != null && item.MenuUrl.Contains("Admin")) || !item.isVisible)
                {
                    itemsToRemove.Add(item);
                }
            }
            foreach (var item in itemsToRemove)
            {
                listMenu.Remove(item);
            }
            List<NavbarItem> navBar = BuildNavbar(listMenu);

            return View("MenuDynamic", navBar);
        }

        private List<NavbarItem> BuildNavbar(List<MenuItem> listMenu)
        {
            var navBar = new List<NavbarItem>();

            foreach (var item in listMenu.Where(m => m.ParentId == null))
            {
                var navbarItem = new NavbarItem
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Title = item.Title,
                    MenuUrl = item.MenuUrl,
                    MenuIndex = item.MenuIndex,
                    isVisible = item.isVisible,
                    subItems = new List<NavbarItem>()
                };

                PopulateSubItems(navbarItem, listMenu);
                navBar.Add(navbarItem);
            }

            return navBar;
        }

        private void PopulateSubItems(NavbarItem parentItem, List<MenuItem> listMenu)
        {
            foreach (var item in listMenu.Where(m => m.ParentId == parentItem.Id))
            {
                var subItem = new NavbarItem
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Title = item.Title,
                    MenuUrl = item.MenuUrl,
                    MenuIndex = item.MenuIndex,
                    isVisible = item.isVisible,
                    subItems = new List<NavbarItem>()
                };

                PopulateSubItems(subItem, listMenu);
                parentItem.subItems.Add(subItem);
            }
        }
    }
}
