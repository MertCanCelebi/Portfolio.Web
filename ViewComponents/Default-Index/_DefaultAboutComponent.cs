using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.ViewComponents.Default_Index
{
    public class _DefaultAboutComponent(PortfolioContext context):ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var abouts = context.Abouts.ToList();
            return View(abouts);
        }
    }
}
