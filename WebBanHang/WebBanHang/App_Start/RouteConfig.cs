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
