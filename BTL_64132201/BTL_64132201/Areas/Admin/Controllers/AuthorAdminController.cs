using BTL_64132201.Areas.Admin.DAL;
using BTL_64132201.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorAdminController : Controller
    {
        AuthorAdminDAL authorDAL = new AuthorAdminDAL();
        // GET: AuthorAdminController
        public ActionResult Index()
        {
            List<AuthorAdmin> authors = new List<AuthorAdmin>();
            authors = authorDAL.getAll();
            return View(authors);
        }

        // GET: AuthorAdminController/Details/5
        public ActionResult Details(int id)
        {
            AuthorAdmin author = new AuthorAdmin();
            author = authorDAL.getAuthorById(id);
            return View(author);
        }

        // GET: AuthorAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorAdmin authorNew)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    DateTime now = DateTime.Now;
                    authorNew.CreateAt = now;
                    authorNew.UpdateAt = now;
                    IsInserted = authorDAL.AddNew(authorNew);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Tạo thành công";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tạo thất bại";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: AuthorAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            AuthorAdmin author = new AuthorAdmin();
            author = authorDAL.getAuthorById(id);
            return View(author);
        }

        // POST: AuthorAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorAdmin authorNew)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);
                    DateTime now = DateTime.Now;
                    authorNew.UpdateAt = now;
                    IsInserted = authorDAL.UpdateAuthorById(authorNew, id);
                    if (IsInserted)
                    {
                        Console.WriteLine("Update Success");
                        TempData["SuccessMessage"] = "Cập nhật thành công";
                    }
                    else
                    {
                        Console.WriteLine("Update Fail");
                        TempData["ErrorMessage"] = "Cập nhật thất bại";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update error " + ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: AuthorAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            AuthorAdmin author = new AuthorAdmin();
            author = authorDAL.getAuthorById(id);
            return View(author);
        }

        // POST: AuthorAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);
                    IsInserted = authorDAL.DeleteAuthorById(id);
                    if (IsInserted)
                    {
                        Console.WriteLine("Xóa thành công");
                        TempData["SuccessMessage"] = "Xóa thành công";
                    }
                    else
                    {
                        Console.WriteLine("Xóa thất bại");
                        TempData["ErrorMessage"] = "Xóa thất bại";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete error " + ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
