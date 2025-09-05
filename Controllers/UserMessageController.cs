using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;

namespace Portfolio.Web.Controllers
{
    public class UserMessageController(PortfolioContext context) : Controller
    {
        public IActionResult Index(string filter = "all")
        {
            var messages = context.UserMassages.AsQueryable();

            if (filter == "read")
                messages = messages.Where(x => x.IsRead);
            else if (filter == "unread")
                messages = messages.Where(x => !x.IsRead);

            // Butonlardaki sayılar için tüm listeyi de çek
            var allCount = context.UserMassages.Count();
            var readCount = context.UserMassages.Count(x => x.IsRead);
            var unreadCount = context.UserMassages.Count(x => !x.IsRead);

            ViewBag.AllCount = allCount;
            ViewBag.ReadCount = readCount;
            ViewBag.UnreadCount = unreadCount;
            ViewBag.Filter = filter;

            
            return View(messages.ToList());
        }
        public IActionResult ChangeStatus(int id)
        {
            var message = context.UserMassages.Find(id);
            if (message != null)
            {
                // mevcut değerini tersine çeviriyoruz
                message.IsRead = !message.IsRead;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteUserMessage(int id)
        {
            var message = context.UserMassages.Find(id);

            context.UserMassages.Remove(message);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult ShowMessage(int id)
        {
            var message = context.UserMassages.Find(id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                context.SaveChanges();
            }

            return PartialView("_ShowMessagePartial", message);
        }
    }
}
