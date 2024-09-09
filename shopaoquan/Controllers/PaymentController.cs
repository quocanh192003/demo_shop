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
            
            if (HttpContext.Session["user"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biến sesion
                var lsCart = (List < cartModel >) Session["cart"];
                //Gán dữ liệu cho biến order
                Models.Order order = new Models.Order();
                order.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                order.UserID = Convert.ToInt32(HttpContext.Session["Id"].ToString());
                

                
                order.CreatedOnUtc = DateTime.Now;
                order.Status = 1;
                db.Orders.Add(order);
                db.SaveChanges();
                //Lấy orderID vừa tạo để lưu vào bảng DeitalOrderId
                int intOrderID = order.Id;

                List<OrderDetail> lstOrderDeital = new List<OrderDetail>();
                foreach(var item in lsCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.IdOrder = intOrderID;
                    obj.ProductId = item.product.id_product;
                    lstOrderDeital.Add(obj);

                }
                db.OrderDetails.AddRange(lstOrderDeital);
                db.SaveChanges();
            }
            return View();
        }
    }
}