using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using shopaoquan.Models;

namespace shopaoquan.Controllers
{
    public class LoginController : Controller
    {
        SHOPAOQUANEntities db = new SHOPAOQUANEntities();
        // GET: Login
        public ActionResult Login()
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();

            ViewBag.subcate = subcate;
            ViewBag.cate = cate;
            return View();
        }

        public ActionResult Register()
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();

            ViewBag.subcate = subcate;
            ViewBag.cate = cate;
            return View();

        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var ck_email = db.Users.FirstOrDefault(p => p.email == user.email);
                if(ck_email != null)
                {
                    TempData["error"] = "Người dùng đã tồn tại!";
                    return RedirectToAction("Login", "Register");
                }
                else
                {
                    db.Users.Add(user);
                    
                    db.SaveChanges();
                    return RedirectToAction("Login", "Login");
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(p => p.email == user.email && p.pass == user.pass);
                if(check != null && check.roles != "2")
                {
                    
                    Session["user"] = check.name_user;

                    
                    return RedirectToAction("Index", "Home");
                }
                
                if (check != null && check.roles == "2")
                {
                    Session["user"] = check.name_user;

                 
                    return RedirectToAction("Index", "Users", new { Area = "Admin" });

                }
                if (check == null)
                {
                    TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác!";
                    return RedirectToAction("Login", "Login");
                }

            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            Session["user"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}