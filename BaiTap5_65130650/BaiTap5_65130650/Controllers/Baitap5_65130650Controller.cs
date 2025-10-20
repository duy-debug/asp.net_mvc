    using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTap5_65130650.Models;

namespace BaiTap5_65130650.Controllers
{
    public class Baitap5_65130650Controller : Controller
    {
        // GET: Baitap5_65130650
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(HttpPostedFileBase Avatar, EmpModel emp)
        {
            //Lấy thông tin từ input type=file có tên Avatar 
            string postedFileName =System.IO.Path.GetFileName(Avatar.FileName); 
            //Lưu hình đại diện về Server 
            var path = Server.MapPath("/Images/"+ postedFileName); 
            Avatar.SaveAs(path); 
            string fSave = Server.MapPath("/emp.txt"); 
            string[] emInfo =  
{emp.EmpID, emp.Name, emp.BirthOfDate.ToShortDateString(), 
                 emp.Email,emp.Password,emp.Department, postedFileName}; 
            //Lưu các thông ti vào tập tin emp.txt 
            System.IO.File.WriteAllLines(fSave, emInfo); 
            //Ghi nhận các thông tin đăng ký để hiện thị trên View Confirm 
            ViewBag.EmpID = emInfo[0]; 
            ViewBag.Name = emInfo[1]; 
            ViewBag.BirthOfDate = emInfo[2].ToString(); 
            ViewBag.Email = emInfo[3]; 
            ViewBag.Password = emInfo[4]; 
            ViewBag.Department = emInfo[5]; 
            ViewBag.Avatar = "/Images/" + emInfo[6]; 
            return View("Confirm");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {   
            string postedFileName =
System.IO.Path.GetFileName(banner.FileName);
            var path = Server.MapPath("/Images/"+postedFileName);   
            banner.SaveAs(path);
            string fSave = Server.MapPath("/banner.txt");
            System.IO.File.WriteAllText(fSave, postedFileName);
            return View();
        }
        [HttpPost]
        public ActionResult SendMail (MailInfo model)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("duy.tmn.65cntt@ntu.edu.vn");
            mail.To.Add(model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("duy.tmn.65cntt@ntu.edu.vn", "zkqp yzyr lsna vrof");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return Content("Đã gửi email.");
        }
        public ActionResult SendMail()
        {
            return View();
        }
    }
}