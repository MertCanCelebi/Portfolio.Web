using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;

namespace Portfolio.Web.Controllers
{
    public class ProjectController(PortfolioContext context) : Controller
    {
        private void CategoryDropdown()
        {
            var categories = context.Categories.ToList();
            ViewBag.Categories = (from x in categories
                                  select new SelectListItem
                                  {
                                      Text = x.CategoryName,
                                      Value = x.CategoryId.ToString()
                                  }).ToList();
        }
        public IActionResult Index()
        {
            var values = context.Projects.Include(x=>x.Category).ToList();
            return View(values);
        }
        public IActionResult CreateProject()
        {
            CategoryDropdown();
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            CategoryDropdown();

            if (!ModelState.IsValid)
            {
                return View(project);
            }
            context.Projects.Add(project);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
