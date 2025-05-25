using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Filters
{
    public class YetkiAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Yetkiler Rol { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["user"] as Kullanici;
            if (user == null || user.Yetki != Rol)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "YetkisizErisim"
                };
            }
        }
    }
}