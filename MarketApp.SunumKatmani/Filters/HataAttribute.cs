using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Filters
{
    public class HataAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "Hata",
                ViewData = new ViewDataDictionary(filterContext.Exception)
            };
            filterContext.ExceptionHandled = true;
        }
    }
}