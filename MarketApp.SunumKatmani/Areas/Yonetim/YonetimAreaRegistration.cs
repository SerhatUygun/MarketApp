using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim
{
    public class YonetimAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Yonetim";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Yonetim_default",
                "Yonetim/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}