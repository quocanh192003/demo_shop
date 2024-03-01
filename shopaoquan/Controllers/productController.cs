using shopaoquan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shopaoquan.Controllers
{
    public class productController : Controller
    {
        SHOPAOQUANEntities db = new SHOPAOQUANEntities();
        // GET: product
        public ActionResult Nam(string selectedCategory)
        {
            List<product> all = db.products.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            List<Category> categories = db.Categories.ToList();
            List<Brand> brands = db.Brands.ToList();
            List<sale> TopPriceMen = db.sales.OrderByDescending(p => p.sale_off).ToList();

            CategoryModel model = new CategoryModel
            {
                SelectedCategory = selectedCategory
            };


            ViewBag.TopPriceMen = TopPriceMen;
            ViewBag.all = all;
            ViewBag.subcate = subcate;
            ViewBag.cate = categories;
            ViewBag.Brand = brands;
            return View(model);
        }

        public ActionResult Detail(string MASANPHAM)
        {
            List<product> all = db.products.ToList();
            List<sale> sale (int count)
            {
                return db.sales.OrderByDescending(p => p.sale_off).Take(count).ToList();
            }
            List<size> size = db.sizes.ToList();
            product detailproduct = db.products.SingleOrDefault(p => p.id_product == MASANPHAM);
            List<Category> categories = db.Categories.ToList();
            List<subCategory> subCategories = db.subCategories.ToList();

            ViewBag.detailproducts = detailproduct;
            ViewBag.sale = sale(5);
            ViewBag.size = size;
            ViewBag.all = all;
            ViewBag.cate = categories;
            ViewBag.subcate = subCategories;
            return View();
        }
       
        
    }
}