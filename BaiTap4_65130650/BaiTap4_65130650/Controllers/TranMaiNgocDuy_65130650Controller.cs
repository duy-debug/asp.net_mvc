using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTap4_65130650.Models;

namespace BaiTap4_65130650.Controllers
{
    public class TranMaiNgocDuy_65130650Controller : Controller
    {
        // GET: TranMaiNgocDuy_65130650
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        public ActionResult Index(MailInfo model) 
        { 
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(); 
        mail.From = new System.Net.Mail.MailAddress("duy.tmn.65cntt@ntu.edu.vn"); 
        mail.To.Add(model.To); 
        mail.Subject = model.Subject; 
        mail.Body = model.Body; 
        mail.IsBodyHtml = true; 
        System.Net.Mail.SmtpClient smtp=new System.Net.Mail.SmtpClient("smtp.gmail.com",587); 
        smtp.Credentials=new System.Net.NetworkCredential("duy.tmn.65cntt@ntu.edu.vn", "zkqp yzyr lsna vrof"); 
        smtp.EnableSsl = true; 
        smtp.Send(mail); 
        return Content("Đã gửi email."); 
} 
    }
}