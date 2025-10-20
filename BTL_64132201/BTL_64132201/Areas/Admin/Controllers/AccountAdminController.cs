using BTL_64132201.Areas.Admin.DAL;
using BTL_64132201.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_64132201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountAdminController : Controller
    {
        AccountAdminDAL accountDAL = new AccountAdminDAL();
        // GET: AccountAdminController
        public ActionResult Index()
        {
            List<AccountAdmin> list = new List<AccountAdmin>();
            list = accountDAL.GetListAccount();
            return View(list);
        }

        // GET: AccountAdminController/Details/5
        public ActionResult Details(int id)
        {
            AccountAdmin accountAdmin = new AccountAdmin();
            accountAdmin = accountDAL.GetAccountById(id);
            return View(accountAdmin);
        }

        // GET: AccountAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            AccountAdmin accountAdmin = new AccountAdmin();
            accountAdmin = accountDAL.GetAccountById(id);
            return View(accountAdmin);
        }

        // POST: AccountAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AccountAdmin account)
        {
            try
            {
                bool IsInserted = false;
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Update Category Id = ", id);
                    DateTime now = DateTime.Now;
                    account.UpdateAt = now;
                    IsInserted = accountDAL.UpdateAccount(account, id);
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

        // GET: AccountAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            AccountAdmin accountAdmin = new AccountAdmin();
            accountAdmin = accountDAL.GetAccountById(id);
            return View(accountAdmin);
        }

        // POST: AccountAdminController/Delete/5
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
                    IsInserted = accountDAL.DeleteAccount(id);
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
