using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{
    [Authorize]
    public class StatisticsController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            ViewBag.projectCount = context.Projects.Count();
            ViewBag.categoryCount = context.Categories.Count();
            ViewBag.userCount = context.Users.Count();
            ViewBag.skillAvarege = context.Skills.Any() ? context.Skills.Average(x => x.Percentage).ToString("00.00") : 0.0.ToString("00.00");
            ViewBag.unreadMessageCount = context.UserMassages.Count(x => x.IsRead == false);
            ViewBag.readMessageCount = context.UserMassages.Count(x => x.IsRead == true);
            ViewBag.lastMessageOwner = context.UserMassages.OrderByDescending(x => x.UserMassageId).Select(y => y.Name).FirstOrDefault();

            var startYear = context.Experiences.Min(x => x.StartYear);
            ViewBag.experienceYear = DateTime.Now.Year - startYear;

            ViewBag.companyCount = context.Experiences.Select(x => x.Company).Distinct().Count();

            ViewBag.reviewAverage = context.Testimonials.Any() ? context.Testimonials.Average(x => x.Review).ToString("0.0") : "Değerlendirme Yapılmadı";
            
            ViewBag.maxReviewOwner = context.Testimonials.OrderByDescending(x=>x.Review).Select(y => y.Name).FirstOrDefault();

            ViewBag.testimonialCount = context.Testimonials.Count();

            return View();
        }
    }
}
