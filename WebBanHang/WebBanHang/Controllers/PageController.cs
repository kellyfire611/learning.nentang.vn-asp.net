using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("gioi-thieu")]
        public ActionResult GioiThieu()
        {
            ViewBag.Message = "Trang giới thiệu";

            return View();
        }

        public ActionResult LienHe()
        {
            ViewBag.Message = "Trang liên hệ";

            return View();
        }
    }
}