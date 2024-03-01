using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Models;

namespace shopaoquan.Controllers
{
    public class CartController : Controller
    {
        SHOPAOQUANEntities db = new SHOPAOQUANEntities();
        // GET: Cart
        public ActionResult Index()
        {
            

            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();

            ViewBag.subcate = subcate;
            ViewBag.cate = cate;
            return View((List<cartModel>)Session["cart"]);
        }

        [HttpPost]
        public ActionResult AddToCart(string id, int quantity)
        {
           

                if (Session["cart"] == null)
                {
                    List<cartModel> cart = new List<cartModel>();

                    cart.Add(new cartModel { product = db.products.Find(id), Quantity = quantity });
                    Session["cart"] = cart;
                    Session["count"] = 1;


                }
                else
                {
                    List<cartModel> cart = (List<cartModel>)Session["cart"];
                    //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                    int index = isExist(id);
                    if (index != -1)
                    {
                        //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                        cart[index].Quantity += quantity;
                    }
                    else
                    {
                        //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                        cart.Add(new cartModel { product = db.products.Find(id), Quantity = quantity });
                        //Tính lại số sản phẩm trong giỏ hàng
                        Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    }
                    Session["cart"] = cart;
                }
                return Json(new { Message = "Thành công", Count = Session["count"], JsonRequestBehavior.AllowGet });
            
           
        }
        private int isExist(string id)
        {
            List<cartModel> cart = (List<cartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.id_product.Equals(id))
                    return i;
            return -1;
        }

        //Xóa giỏ hàng
        public ActionResult Remove(string id)
        {
            List<cartModel> remove = (List<cartModel>)Session["cart"];
            remove.RemoveAll(p => p.product.id_product == id);
            Session["cart"] = remove;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { message = "Xoa thanh cong", JsonRequestBehavior.AllowGet });
        }
    }
}

