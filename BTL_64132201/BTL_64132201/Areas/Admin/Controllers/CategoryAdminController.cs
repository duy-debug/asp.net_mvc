using BTL_64132201.Areas.Admin.DAL;
using BTL_64132201.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryAdminController : Controller
    {
        // GET: CategoryAdminController
        CategoryAdminDAL categoryAdminDAL = new CategoryAdminDAL();
        public ActionResult Index()
        {
            List<CategoryAdmin> categories = new List<CategoryAdmin>();
            categories = categoryAdminDAL.getAll();
            return View(categories);
        }

        // GET: CategoryAdminController/Details/5
        public ActionResult Details(int id)
        {
            CategoryAdmin category = new CategoryAdmin();
            category = categoryAdminDAL.getCategoryById(id);
            return View(category);
        }

        // GET: CategoryAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryAdmin categoryNew)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryNew.CreateAt = DateTime.Now;
                    categoryNew.UpdateAt = DateTime.Now;

                    bool isInserted = categoryAdminDAL?.AddNew(categoryNew) ?? false;

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Tạo thành công";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tạo thất bại";
                    }
                }

                // If we reach here, either ModelState is invalid or insertion failed
                return View(categoryNew);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the category: " + ex.Message;

                // Log the exception for debugging purposes
                // e.g., Logger.LogError(ex, "Error in Create");

                return View(categoryNew);
            }
        }

        // GET: CategoryAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            CategoryAdmin category = new CategoryAdmin();
            category = categoryAdminDAL.getCategoryById(id);
            return View(category);
        }

        // POST: CategoryAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryAdmin categoryNew)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);
                    DateTime now = DateTime.Now;
                    categoryNew.UpdateAt = now;
                    IsInserted = categoryAdminDAL.updateCategoryById(id, categoryNew);
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

        // GET: CategoryAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryAdmin category = new CategoryAdmin();
            category = categoryAdminDAL.getCategoryById(id);
            return View(category);
        }

        // POST: CategoryAdminController/Delete/5
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
                    IsInserted = categoryAdminDAL.deleteCategoryById(id);
                    if (IsInserted)
                    {
                        Console.WriteLine("Delete Success");
                        TempData["SuccessMessage"] = "Xóa thành công";
                    }
                    else
                    {
                        Console.WriteLine("Delete Fail");
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
