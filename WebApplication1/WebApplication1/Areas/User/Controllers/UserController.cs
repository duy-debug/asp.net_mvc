using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.User.Controllers
{
    public class UserController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // Kiểm tra quyền User trước mỗi action
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "User")
            {
                filterContext.Result = RedirectToAction("Login", "Account", new { area = "" });
            }
            base.OnActionExecuting(filterContext);
        }

        // GET: User/User
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.users.Find(userId);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            ViewBag.Username = user.username;
            ViewBag.Role = user.Role;
            return View(user);
        }

        // GET: User/User/Profile
        public ActionResult Profile()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.users.Find(userId);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return View(user);
        }

        // GET: User/User/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: User/User/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu mới và xác nhận không khớp";
                return View();
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            var user = db.users.Find(userId);

            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            // Kiểm tra mật khẩu cũ
            string oldPasswordHash = HashPassword(oldPassword);
            if (user.password_hash != oldPasswordHash)
            {
                ViewBag.Error = "Mật khẩu cũ không đúng";
                return View();
            }

            // Cập nhật mật khẩu mới
            user.password_hash = HashPassword(newPassword);
            db.SaveChanges();

            ViewBag.Success = "Đổi mật khẩu thành công!";
            return View();
        }

        // Helper method để hash password
        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}