using BTL_64132201.Helper;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Lấy danh sách giỏ h
            var cart = HttpContext.Session.Get<List<CartItem>>(MyConst.CART_KEY) ?? new List<CartItem>();
            return View(new CartModel()
            {
                Quantity = cart.Sum(p => p.Quantity),
                Total = cart.Sum(p => p.Total)
            });
        }
    }
}
