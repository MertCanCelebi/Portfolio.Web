using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    [AllowAnonymous]
    public class BannerController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var banner = context.Banners.ToList();
            return View(banner);
        }
        [HttpGet]
        public IActionResult CreateBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBanner(Banner banner )
        {

            context.Banners.Add(banner); 
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteBanner(int id)
        {
            var banner = context.Banners.Find(id);
            context.Banners.Remove(banner);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateBanner(int id)
        {
            var banner = context.Banners.Find(id);
            return View(banner);
        }
        [HttpPost]
        public IActionResult UpdateBanner(Banner banner)
        {
            context.Banners.Update(banner);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
