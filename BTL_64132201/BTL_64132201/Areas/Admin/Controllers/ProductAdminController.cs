using BTL_64132201.Areas.Admin.DAL;
using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL_64132201.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ProductAdminController : Controller
    {
        ProductAdminDAL productDAL =new ProductAdminDAL();
        CategoryAdminDAL categoryDAL = new CategoryAdminDAL();
        AuthorAdminDAL authorDAL = new AuthorAdminDAL();
        // GET: ProductAdminController
        public ActionResult Index(int page = 1, string searchString = "", string sortOrder = "")
        {
            int pageSize = 5;
            ViewData["CurrentFilter"] = searchString;
            List<ProductAdmin> products = new List<ProductAdmin>();
            ViewData["SortColumn"] = sortOrder;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "title" ? "title_desc" : "title";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["RateSortParm"] = sortOrder == "rate" ? "rate_desc" : "rate";
            products = productDAL.getProduct_Pagination(page, pageSize, searchString.Trim(), sortOrder);
            int numRows = productDAL.getCountRow_Pagination(page, pageSize, searchString.Trim());
            double pageCount = (double)numRows / pageSize;
            int maxPage = (int)Math.Ceiling(pageCount);
            ProductAdminModel model = new ProductAdminModel();
            model.ProductAdmins = products;
            model.CurrentPageIndex = page;
            model.PageCount = maxPage;
            return View(model);
        }

        // GET: ProductAdminController/Details/5
        public ActionResult Details(int id)
        {
            ProductAdmin productAdmin = new ProductAdmin();
            productAdmin = productDAL.GetProductById(id);
            return View(productAdmin);
        }

        // GET: ProductAdminController/Create
        public ActionResult Create()
        {
            List<CategoryAdmin> categories = new List<CategoryAdmin>();
            List<AuthorAdmin> authors = new List<AuthorAdmin>();
            // Lấy danh sách Category từ DataBase
            categories = categoryDAL.getAll();
            authors = authorDAL.getAll();
            // Khai báo Model
            ProductFormAdmin productAddNew = new ProductFormAdmin();
            // Tạo danh sách Input Select
            productAddNew.ListCategory = new List<SelectListItem>();
            productAddNew.ListAuthor = new List<SelectListItem>();
            foreach (var item in categories)
            {
                productAddNew.ListCategory.Add(
                new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                }
                );
            }
            foreach (var item in authors)
            {
                productAddNew.ListAuthor.Add(
                new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                }
                );
            }
            return View(productAddNew);
        }

        // POST: ProductAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFormAdmin productAddNew, IFormFile Img)
        {
            try
            {
                //tự động lấy thời gian CreateAt và UpdateAt theo giờ hệ thống
                DateTime now = DateTime.Now;
                productAddNew.CreateAt = now;
                productAddNew.UpdateAt = now;
                //Upload Hinh
                if (Img == null)
                {
                    productAddNew.Img = "";
                }
                else
                {
                    var ImageName = ImageHelper.UploadImage(Img, "SanPham");
                    productAddNew.Img = ImageName;
                }
                //lấy Id Category
                int IdCategory = Convert.ToInt32(productAddNew.IdCategorySelected);
                productAddNew.CategoryId = IdCategory;
                int IdAuthor = Convert.ToInt32(productAddNew.IdAuthorSelected);
                productAddNew.AuthorId = IdAuthor;
                // truy vấn tới CSDL
                bool IsInserted = productDAL.AddNew(productAddNew);
                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsInserted)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Insert Product Success");
                    TempData["SuccessMessage"] = "Thêm thành công";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Insert Product Fail");
                    TempData["ErrorMessage"] = "Thêm thất bại";
                    return View();
                }
            }
            catch (Exception ex)
            {
                //Error
                Console.WriteLine("Insert Product error ", ex.Message);
                return View();
            }
        }

        // GET: ProductAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductAdmin product = new ProductAdmin();
            product = productDAL.GetProductById(id);

            ProductFormAdmin ProductFormAdmin = new ProductFormAdmin();
            ProductFormAdmin.Id = id;
            ProductFormAdmin.Title = product.Title;
            ProductFormAdmin.Content = product.Content;
            ProductFormAdmin.Img = product.Img;
            ProductFormAdmin.Price = product.Price;
            ProductFormAdmin.CreateAt = product.CreateAt;
            ProductFormAdmin.UpdateAt = product.UpdateAt;
            ProductFormAdmin.Rate = product.Rate;
            ProductFormAdmin.CategoryId = product.CategoryId;
            ProductFormAdmin.CategoryTitle = product.CategoryTitle;

            List<CategoryAdmin> categories = new List<CategoryAdmin>();
            categories = categoryDAL.getAll();

            List<AuthorAdmin> authors = new List<AuthorAdmin>();
            authors = authorDAL.getAll();

            ProductFormAdmin.ListCategory = new List<SelectListItem>();
            foreach (var item in categories)
            {
                ProductFormAdmin.ListCategory.Add(
                new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                }
                );
            }
            ProductFormAdmin.ListAuthor = new List<SelectListItem>();
            foreach (var item in authors)
            {
                ProductFormAdmin.ListAuthor.Add(
                new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                }
                );
            }
            return View(ProductFormAdmin);
        }

        // POST: ProductAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductFormAdmin productEdit, IFormFile ImageUpload)
        {
            try
            {
                DateTime now = DateTime.Now;
                productEdit.UpdateAt = now;
                //Nếu có hình ảnh được Upload
                if (ImageUpload != null)
                {
                    //Upload Hinh
                    var ImageName = ImageHelper.UploadImage(ImageUpload, "SanPham");
                    productEdit.Img = ImageName;
                }
                //lấy Id Category
                int IdCategory = Convert.ToInt32(productEdit.IdCategorySelected);
                productEdit.CategoryId = IdCategory;
                int IdAuthor = Convert.ToInt32(productEdit.IdAuthorSelected);
                productEdit.AuthorId = IdAuthor;
                // truy vấn tới CSDL
                bool IsInserted = productDAL.UpdateProduct(productEdit, id);
                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsInserted)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Cập nhật thông tin thành công");
                    TempData["SuccessMessage"] = "Update Success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("Cập nhật thông tin thành công");
                    TempData["ErrorMessage"] = "Update Fail";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            ProductAdmin product = new ProductAdmin();
            product = productDAL.GetProductById(id);
            return View(product);
        }

        // POST: ProductAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var IsSuccess = productDAL.DeleteProduct(id);
                // Kiểm tra truy vấn SQL thành công hay không?
                if (IsSuccess)
                {
                    // Truy vấn Thành công
                    Console.WriteLine("Xóa sản phẩm thành công");
                    TempData["SuccessMessage"] = "Xóa thành công";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Truy vấn Thất bại
                    Console.WriteLine("Xóa sản phẩm thất bại");
                    TempData["ErrorMessage"] = "Xóa thất bại";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
