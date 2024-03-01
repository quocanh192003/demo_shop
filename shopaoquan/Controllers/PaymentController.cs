using shopaoquan.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class PaymentController : Controller
    {
        SHOPAOQUANEntities db = new SHOPAOQUANEntities();
        // GET: Payment
        public ActionResult Payment()
        {
            if (Session["id_user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biến sesion
                var lsCart = (List < cartModel >) Session["cart"];
                //Gán dữ liệu cho biến order
                Order order = new Order();
                order.

            }
            return View();
        }
    }
}