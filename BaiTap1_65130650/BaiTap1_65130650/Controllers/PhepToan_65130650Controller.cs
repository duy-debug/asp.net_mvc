using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiTap1_65130650.Models;

namespace BaiTap1_65130650.Controllers
{
    public class PhepToan_65130650Controller : Controller
    {
        // GET: PhepToan_65130650
        public ActionResult UseRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UseRequest(CalModels cal)
        {
            switch (cal.pt)
            {
                case "+": ViewBag.KQ = cal.a + cal.b; break;
                case "-": ViewBag.KQ = cal.a - cal.b; break;
                case "*": ViewBag.KQ = cal.a * cal.b; break;
                case "/": 
            if (cal.b == 0) ViewBag.KQ = "Không chia được cho 0";
            else ViewBag.KQ = cal.a / cal.b; break;
            }
            return View();
        }

    }
}