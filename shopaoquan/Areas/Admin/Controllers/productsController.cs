using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using shopaoquan.Models;

namespace shopaoquan.Areas.Admin.Controllers
{
    public class productsController : Controller
    {
         SHOPAOQUANEntities db = new SHOPAOQUANEntities();


        // GET: Admin/products
        public ActionResult Index(string CurrentFilter,string searchString, int? page)
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();

            ViewBag.cate = cate;
            ViewBag.subcate = subcate;

            var listproduct = new List<product>();
            listproduct = db.products.Include(p => p.Brand).Include(p => p.subCategory).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
                listproduct = db.products.Where(n => n.name_product.Contains(searchString)).ToList();
            }
            else
            {
                searchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            listproduct = db.products.OrderBy(p => p.id_product).ToList();
            return View(listproduct.ToPagedList(pageNumber, pageSize));
        }
       

        // GET: Admin/products/Details/5
        public ActionResult Details(string id)
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.cate = cate;
            ViewBag.subcate = subcate;

            return View(product);
        }

        // GET: Admin/products/Create
        public ActionResult Create()
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            ViewBag.cate = cate;
            ViewBag.subcate = subcate;

            ViewBag.id_brand = new SelectList(db.Brands, "id_brand", "brand_name");
            ViewBag.id_subcate = new SelectList(db.subCategories, "id_subcate", "name_subcate");
            return View();
        }

        // POST: Admin/products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stt,id_product,name_product,images,price_new,price_old,describe,quantity,id_subcate,id_brand")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_brand = new SelectList(db.Brands, "id_brand", "brand_name", product.id_brand);
            ViewBag.id_subcate = new SelectList(db.subCategories, "id_subcate", "name_subcate", product.id_subcate);
            return View(product);
        }

        // GET: Admin/products/Edit/5
        public ActionResult Edit(string id)
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            ViewBag.cate = cate;
            ViewBag.subcate = subcate;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_brand = new SelectList(db.Brands, "id_brand", "brand_name", product.id_brand);
            ViewBag.id_subcate = new SelectList(db.subCategories, "id_subcate", "name_subcate", product.id_subcate);
            return View(product);
        }

        // POST: Admin/products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stt,id_product,name_product,images,price_new,price_old,describe,quantity,id_subcate,id_brand")] product product)
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            ViewBag.cate = cate;
            ViewBag.subcate = subcate;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_brand = new SelectList(db.Brands, "id_brand", "brand_name", product.id_brand);
            ViewBag.id_subcate = new SelectList(db.subCategories, "id_subcate", "name_subcate", product.id_subcate);
            return View(product);
        }

        // GET: Admin/products/Delete/5
        public ActionResult Delete(string id)
        {
            List<Category> cate = db.Categories.ToList();
            List<subCategory> subcate = db.subCategories.ToList();
            ViewBag.cate = cate;
            ViewBag.subcate = subcate;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
