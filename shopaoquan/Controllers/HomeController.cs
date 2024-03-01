using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shopaoquan.Models;
namespace shopaoquan.Controllers
{
    public class HomeController : Controller
    {
        SHOPAOQUANEntities db = new SHOPAOQUANEntities();
        public ActionResult Index()
        {
            List<product> productall = db.products.ToList();
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            List<product> spnhieu = sanphamnhieu(20);
            

            /*Lấy ra sản phẩm có số lượng nhiều nhất*/
            List<product> sanphamnhieu(int count)
            {
                return db.products.OrderByDescending(p => p.quantity).Take(count).ToList();
            }

            /*Lấy ra sản phẩm ngẫu nhiên*/
            List<product> randomsp(int count)
            {
                Random rd = new Random();
                
                List<int> randomIndexes = Enumerable.Range(0, productall.Count).OrderBy(x => rd.Next()).Take(count).ToList();
                List<product> randomProducts = randomIndexes.Select(index => productall[index]).ToList();
                return randomProducts;
            }

            /*Lấy ra sản phẩm có gia sale lớn nhất*/
            List<sale> sale(int count)
            {
                return db.sales.OrderByDescending(p => p.sale_off).Take(count).ToList();
            }
            /*Lấy sản phẩm ra và xếp theo giá từ cao đến thấp*/

            List<product> product;
            product = db.Database.SqlQuery<product>("GetTop3ProductsPerSubcategory").Take(12).ToList();

            List<product> price_big = GetTopNProductsPerSubcategory(db,5);


            ViewBag.price_big = price_big;
            ViewBag.product = product;
            ViewBag.sale = sale(3);
            ViewBag.subcate = subcate;
            ViewBag.cate = cate;
            ViewBag.productall = productall;
            ViewBag.spnhieu = spnhieu;
            ViewBag.randomproduct = randomsp(9);
            return View();

            
        }

        /* Lấy ra 5 sp có giá cao nhất*/
        private List<product> GetTopNProductsPerSubcategory (SHOPAOQUANEntities db, int n)
        {
            var parameter = new SqlParameter("@N", n);
            List<product> price_big = db.Database.SqlQuery<product>("select * from dbo.GetTopNProductsPerSubcategory(@N)", parameter).ToList();
            return price_big;
        }
        

        
    }
}