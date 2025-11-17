using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiGK65CNTT2_65130650.Models;

namespace ThiGK65CNTT2_65130650.Controllers
{
    public class TinTucs_65130650Controller : Controller
    {
        private Thi_65130650Entities db = new Thi_65130650Entities();

        // GET: TinTucs_65130650
        public ActionResult Gioithieu_65130650()
        {
            return View();
        }
        public ActionResult Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.LoaiTinTuc);
            return View(tinTucs.ToList());
        }

        // GET: TinTucs_65130650/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: TinTucs_65130650/Create
        public ActionResult Create()
        {
            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT");
            return View();
        }
        // GET: TinTucs_65130650/TimKiem
        [HttpGet]
        public ActionResult TimKiem(string tieuDe = "", string tenLTT = "")
        {
            // Đổ dữ liệu cho dropdown
            ViewBag.tenLTT = new SelectList(db.LoaiTinTucs, "TenLTT", "TenLTT");
            ViewBag.tieuDe = tieuDe;

            // Hiển thị toàn bộ danh sách tin tức ban đầu
            var tinTucs = db.TinTucs.Include(t => t.LoaiTinTuc).ToList();
            return View(tinTucs);
        }



        // POST: TinTucs_65130650/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTT,TieuDe,NgonNgu,NgayDang,NguoiDang,AnhDaiDien,MaLTT,GhiChu")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT", tinTuc.MaLTT);
            return View(tinTuc);
        }

        // GET: TinTucs_65130650/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT", tinTuc.MaLTT);
            return View(tinTuc);
        }

        // POST: TinTucs_65130650/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTT,TieuDe,NgonNgu,NgayDang,NguoiDang,AnhDaiDien,MaLTT,GhiChu")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLTT = new SelectList(db.LoaiTinTucs, "MaLTT", "TenLTT", tinTuc.MaLTT);
            return View(tinTuc);
        }

        // GET: TinTucs_65130650/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: TinTucs_65130650/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
