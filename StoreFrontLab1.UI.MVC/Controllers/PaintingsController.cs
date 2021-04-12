using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab1.DATA.EF;
using StoreFrontLab1.UI.MVC.Utilities;

namespace StoreFrontLab1.UI.MVC.Controllers
{
    public class PaintingsController : Controller
    {
        private AllieasArtGalleryEntities db = new AllieasArtGalleryEntities();

        // GET: Paintings
        [AllowAnonymous]
        public ActionResult Index()
        {
            var paintings = db.Paintings.Include(p => p.Size).Include(p => p.Status);
            return View(paintings.ToList());
        }

        // GET: Paintings/Details/5
        [AllowAnonymous]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "PaintingID,PaintingTitle,SizeID,StatusID,Description,PaintingImg,Price")] Painting painting, HttpPostedFileBase paintingImage)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload
                string file = "NoImage.png";
                
                if (paintingImage != null)
                {
                    file = paintingImage.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    if (goodExts.Contains(ext.ToLower()) && paintingImage.ContentLength<=4194304)//file size must be 4 mb or less
                    {
                        file = Guid.NewGuid() + ext;
                        //ReziseImage
                        string savePath = Server.MapPath("~/Content/img/paintings/");

                        Image convertedImage = Image.FromStream(paintingImage.InputStream);
                        int maxImageSize = 720;
                        int maxThumbSize = 200;

                        ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                }

                painting.PaintingImg = file;

                #endregion


                db.Paintings.Add(painting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "PaintingID,PaintingTitle,SizeID,StatusID,Description,PaintingImg,Price")] Painting painting, HttpPostedFileBase paintingImage)
        {
            if (ModelState.IsValid)
            {

                #region Image Upload
                string file = "NoImage.png";

                if (paintingImage != null)
                {
                    file = paintingImage.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    if (goodExts.Contains(ext.ToLower()) && paintingImage.ContentLength <= 4194304)//file size must be 4 mb or less
                    {
                        file = Guid.NewGuid() + ext;
                        //ReziseImage
                        string savePath = Server.MapPath("~/Content/img/paintings/");

                        Image convertedImage = Image.FromStream(paintingImage.InputStream);
                        int maxImageSize = 720;
                        int maxThumbSize = 200;

                        ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                }

                painting.PaintingImg = file;

                #endregion

                db.Entry(painting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SizeID = new SelectList(db.Sizes, "SizeID", "SizeName", painting.SizeID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", painting.StatusID);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
