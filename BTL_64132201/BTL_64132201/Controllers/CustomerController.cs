using BTL_64132201.DAL;
using BTL_64132201.Helper;
using BTL_64132201.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_64132201.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDAL customerDAL = new CustomerDAL();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            const int idTam = 1;
            Customer? customer = new Customer();
            customer = customerDAL.GetCustomerById(idTam);
            if (customer == null)
            {
                return Redirect("/404");
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult UpdateDetailCustomer(Customer customerUpdate, IFormFile ImgUpload)
        {
            DateTime now = DateTime.Now;
            customerUpdate.UpdateAt = now;
            if (ImgUpload != null)
            {
                var ImageName = ImageHelper.UploadImage(ImgUpload, "KhachHang");
                customerUpdate.Img = ImageName;
            }
            else if (customerUpdate.Img == null)
            {
                customerUpdate.Img = "";
            }
            if (customerUpdate.Address == null)
            {
                customerUpdate.Address = "";
            }
            bool isSuccess = customerDAL.UpdateDetailCustomer(customerUpdate, customerUpdate.Id);
            if (isSuccess)
            {
                Console.WriteLine("Update Customer Success");
                TempData["ProfileSuccessMessage"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Profile");
            }
            else
            {
                Console.WriteLine("Update Customer Fail");
                TempData["ProfileErrorMessage"] = "Lỗi hệ thống";
                return RedirectToAction("Profile");
            }
        }

        #region SIGN_UP
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Customer customerSignUp, IFormFile ImgUpload)
        {
            try
            {
                Customer? customerExist = new Customer();
                customerExist = customerDAL.GetCustomerByEmail(customerSignUp.Email);
                if (customerExist != null)
                {
                    TempData["SignUpErrorMessage"] = "Email đã được đăng ký cho tài khoản khác";
                    return View();
                }
                DateTime now = DateTime.Now;
                customerSignUp.RegisterAt = now;
                customerSignUp.UpdateAt = now;
                if (ImgUpload != null)
                {
                    var ImageName = ImageHelper.UploadImage(ImgUpload, "KhachHang");
                    customerSignUp.Img = ImageName;
                }
                else
                {
                    customerSignUp.Img = "avatar-default.jpg";
                }
                if (customerSignUp.Address == null)
                {
                    customerSignUp.Address = "";
                }
                bool isSuccess = customerDAL.SignUp(customerSignUp);
                if (isSuccess)
                {
                    Console.WriteLine("Update Customer Success");
                    TempData["SignInSuccessMessage"] = "Đăng ký thành công";
                    return RedirectToAction("SignIn");
                }
                else
                {
                    Console.WriteLine("Update Customer Fail");
                    TempData["SignUpErrorMessage"] = "Lỗi hệ thống";
                    return RedirectToAction("SignUp");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }
        }
        #endregion

        #region SIGN_IN
        public IActionResult SignIn(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(CustomerSignIn customerSignIn, string? ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ReturnUrl = ReturnUrl;
                    Customer? customer = new Customer();
                    customer = customerDAL.GetCustomerByEmail(customerSignIn.Email);
                    if (customer == null)
                    {
                        ModelState.AddModelError("Lỗi", "Không có khách hàng này");
                    }
                    else
                    {
                        if (customer.IsActive == 0)
                        {
                            ModelState.AddModelError("Thông báo", "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ admin");
                        }
                        else
                        {
                            if (customer.Password != customerSignIn.Password)
                            {
                                ModelState.AddModelError("Thông báo", "Sai mật khẩu");
                            }
                            else
                            {
                                var claims = new List<Claim>
                                     {
                                     new Claim("CustomerEmail", customer.Email),
                                     new Claim(ClaimTypes.Name, customer.FirstName),
                                     new Claim("CustomerFirstName", customer.FirstName),
                                     new Claim("CustomerLastName", customer.LastName),
                                     new Claim("CustomerId", customer.Id.ToString()),
                                     new Claim(ClaimTypes.Role, customer.Role == 1 ? "Administrator" : "Customer"),
                                     };
                                var claimIdentity = new ClaimsIdentity(claims, "login");
                                var claimPricipal = new ClaimsPrincipal(claimIdentity);
                                await HttpContext.SignInAsync(claimPricipal);
                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return Redirect("/");
                                }
                            }
                        }
                    }
                    return View();
                }
                else return View();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return View();
            }
        }
        #endregion

        #region SIGN_OUT
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        #endregion
    }
}
