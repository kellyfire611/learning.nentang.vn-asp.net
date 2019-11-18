using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET
        public ActionResult ProductDetail(int idProduct)
        {
            products product = null;
            // Lấy dữ liệu Sản phẩm bởi idProduct
            using (QuanLyBanHangEntities context = new QuanLyBanHangEntities())
            {
                product = context.products.Where(p => p.id == idProduct).FirstOrDefault();
            }

            // Truyền dữ liệu từ Controller sang View
            ViewBag.SanPham = product;

            return View();
        }
    }
}