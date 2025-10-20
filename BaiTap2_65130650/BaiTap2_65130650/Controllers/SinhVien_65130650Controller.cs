using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BaiTap2_65130650.Controllers
{
    public class SinhVien_65130650Controller : Controller
    {
        // GET: SinhVien_65130650
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register1(FormCollection field)
        {
            ViewBag.Id = field["Id"];
            ViewBag.Name = field["Name"];
            ViewBag.Marks = field["Marks"];
            return View();
        }
    }
}