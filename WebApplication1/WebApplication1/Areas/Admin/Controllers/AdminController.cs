using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // Kiểm tra quyền Admin trước mỗi action
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                filterContext.Result = RedirectToAction("Login", "Account", new { area = "" });
            }
            base.OnActionExecuting(filterContext);
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            ViewBag.Username = Session["Username"];
            ViewBag.TotalUsers = db.users.Count();
            ViewBag.TotalAdmins = db.users.Count(u => u.Role == "Admin");
            ViewBag.TotalRegularUsers = db.users.Count(u => u.Role == "User");
            return View();
        }

        // GET: Admin/Admin/Users
        public ActionResult Users()
        {
            var users = db.users.ToList();
            return View(users);
        }

        // GET: Admin/Admin/CreateUser
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Admin/Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(string username, string password, string role)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            if (db.users.Any(u => u.username == username))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại";
                return View();
            }

            var newUser = new user
            {
                username = username,
                password_hash = HashPassword(password),
                Role = role ?? "User"
            };

            db.users.Add(newUser);
            db.SaveChanges();

            return RedirectToAction("Users");
        }

        // GET: Admin/Admin/EditUser/5
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(int id, string username, string password, string role)
        {
            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Error = "Tên đăng nhập không được để trống";
                return View(user);
            }

            // Kiểm tra username trùng (trừ user hiện tại)
            if (db.users.Any(u => u.username == username && u.id != id))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại";
                return View(user);
            }

            user.username = username;
            user.Role = role ?? user.Role;

            // Chỉ cập nhật password nếu có nhập mới
            if (!string.IsNullOrEmpty(password))
            {
                user.password_hash = HashPassword(password);
            }

            db.SaveChanges();
            return RedirectToAction("Users");
        }

        // GET: Admin/Admin/DeleteUser/5
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Admin/Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            var user = db.users.Find(id);
            if (user != null)
            {
                db.users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Users");
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