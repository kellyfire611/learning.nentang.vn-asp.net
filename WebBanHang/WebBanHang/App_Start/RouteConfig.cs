using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // route trang chủ
            // URL: /
            routes.MapRoute(
                name: "page.trang_chu",
                url: "",
                defaults: new { controller = "Page", action = "Index" }
            );

            // route trang Giới thiệu
            // URL: /gioi-thieu
            routes.MapRoute(
                name: "page.gioi_thieu",
                url: "gioi-thieu",
                defaults: new { controller = "Page", action = "GioiThieu" }
            );

            // route trang Liên hệ
            // URL: /lien-he 
            routes.MapRoute(
                name: "page.lien_he",
                url: "lien-he",
                defaults: new { controller = "Page", action = "LienHe" }
            );

            // route trang Đăng nhập
            // URL: /dang-nhap
            routes.MapRoute(
                name: "page.dang_nhap",
                url: "dang-nhap",
                defaults: new { controller = "Page", action = "DangNhap" }
            );

            // route trang Đăng ký
            // URL: /dang-ky
            routes.MapRoute(
                name: "page.dang_ky",
                url: "dang-ky",
                defaults: new { controller = "Page", action = "DangKy" }
            );

            // route trang Sản phẩm chi tiết
            // URL: /dang-ky
            routes.MapRoute(
                name: "product.chitiet",
                url: "sanpham-chitiet",
                defaults: new { controller = "Product", action = "ProductDetail" }
            );

            // Route mặc định của Trang web
            // URL: /
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
