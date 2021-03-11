using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab1.DATA.EF;

namespace StoreFrontLab1.UI.MVC.Controllers
{
    public class PaintingsController : Controller
    {
        private AllieasArtGallaryEntities db = new AllieasArtGallaryEntities();

        // GET: Paintings
        public ActionResult Index()
        {
            var paintings = db.Paintings.Include(p => p.Size).Include(p => p.Status);
            return View(paintings.ToList());
        }

        // GET: Paintings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // GET: Paintings/Create
        public ActionResult Create()
        {
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "Size1");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintingID,PaintingTitle,SizeID,Price,StatusID,PaintingImg")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                db.Paintings.Add(painting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "Size1", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "Size1", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintingID,PaintingTitle,SizeID,Price,StatusID,PaintingImg")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "Size1", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painting painting = db.Paintings.Find(id);
            db.Paintings.Remove(painting);
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
