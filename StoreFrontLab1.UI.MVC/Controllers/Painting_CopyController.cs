using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab1.DATA.EF;
using StoreFrontLab1.UI.MVC.Models;

namespace StoreFrontLab1.UI.MVC.Controllers
{
    public class Painting_CopyController : Controller
    {
        private AllieasArtGalleryEntities db = new AllieasArtGalleryEntities();

        // GET: Painting_Copy
        [AllowAnonymous]
        public ActionResult Index()
        {
            var painting_Copies = db.Painting_Copies.Include(p => p.Painting).Include(p => p.Size).Include(p => p.Status);
            return View(painting_Copies.ToList());
        }

        // GET: Painting_Copy/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting_Copy painting_Copy = db.Painting_Copies.Find(id);
            if (painting_Copy == null)
            {
                return HttpNotFound();
            }
            return View(painting_Copy);
        }

        // GET: Painting_Copy/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.PaintingID = new SelectList(db.Paintings, "PaintingID", "PaintingTitle");
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName");
            return View();
        }

        // POST: Painting_Copy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CopyID,PaintingID,Price,StatusID,SizeID")] Painting_Copy painting_Copy)
        {
            if (ModelState.IsValid)
            {
                db.Painting_Copies.Add(painting_Copy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaintingID = new SelectList(db.Paintings, "PaintingID", "PaintingTitle", painting_Copy.PaintingID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting_Copy.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting_Copy.StatusID);
            return View(painting_Copy);
        }

        // GET: Painting_Copy/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting_Copy painting_Copy = db.Painting_Copies.Find(id);
            if (painting_Copy == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaintingID = new SelectList(db.Paintings, "PaintingID", "PaintingTitle", painting_Copy.PaintingID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting_Copy.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting_Copy.StatusID);
            return View(painting_Copy);
        }

        // POST: Painting_Copy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CopyID,PaintingID,Price,StatusID,SizeID")] Painting_Copy painting_Copy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painting_Copy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaintingID = new SelectList(db.Paintings, "PaintingID", "PaintingTitle", painting_Copy.PaintingID);
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting_Copy.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting_Copy.StatusID);
            return View(painting_Copy);
        }

        // GET: Painting_Copy/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting_Copy painting_Copy = db.Painting_Copies.Find(id);
            if (painting_Copy == null)
            {
                return HttpNotFound();
            }
            return View(painting_Copy);
        }

        // POST: Painting_Copy/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painting_Copy painting_Copy = db.Painting_Copies.Find(id);
            db.Painting_Copies.Remove(painting_Copy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int qty, int paintingID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"]; //unboxing
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            Painting_Copy product = db.Painting_Copies.Where(b => b.PaintingID == paintingID).FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel(qty, product);

                if (shoppingCart.ContainsKey(product.PaintingID))
                {
                    shoppingCart[product.PaintingID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.PaintingID, item);
                }
                Session["cart"] = shoppingCart;

                Session["confirm"] = $"'{product.Painting.PaintingTitle}' (Quantity: {qty}) Added to cart";
            }
     
            return RedirectToAction("Index", "ShoppingCart");
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
