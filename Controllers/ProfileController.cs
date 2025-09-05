using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;
using Portfolio.Web.Models;
using System.Net.WebSockets;

namespace Portfolio.Web.Controllers
{
    public class ProfileController(PortfolioContext context) : Controller
    {
        public IActionResult Index()
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Auth");
            }

            // DB’den kullanıcı bilgilerini çek
            var user = context.Users.FirstOrDefault(x => x.UserName == userName);

            if (user is null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(user);
        }
        public IActionResult EditProfile(int id)
        {
            var user = context.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = context.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            // Mevcut şifre doğru mu?
            if (user.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Mevcut şifre hatalı.");
                return View(model);
            }

            // Yeni şifreler uyuşuyor mu?
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Yeni şifreler uyuşmuyor.");
                return View(model);
            }

            // Şifreyi güncelle
            user.Password = model.NewPassword;
            context.SaveChanges();

            TempData["SuccessMessage"] = "Şifre başarıyla değiştirildi.";
            return RedirectToAction("Index");
        }
    }
}
