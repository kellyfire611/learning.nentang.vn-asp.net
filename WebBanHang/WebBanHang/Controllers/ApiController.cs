using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class ApiController : System.Web.Mvc.Controller
    {
        // http://domain.com/api/products
        public System.Web.Mvc.ActionResult GetProducts()
        {
            dynamic lstProduct = null;

            // Lấy dữ liệu danh sách Sản phẩm
            // Entity Framework
            using (QuanLyBanHangEntities context = new QuanLyBanHangEntities())
            {
                lstProduct = (from p in context.products
                             select new
                             {
                                 p.id,
                                 p.product_code,
                                 p.product_name,
                                 p.standard_cost,
                                 p.list_price
                             }).ToList();
            }

            return Json(lstProduct, JsonRequestBehavior.AllowGet);
        }
    }
}
