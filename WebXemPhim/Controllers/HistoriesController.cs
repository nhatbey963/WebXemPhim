using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class HistoriesController : Controller
    {
        private WebXemPhimEntities db = new WebXemPhimEntities();

        // GET: Histories
        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.User).Include(h => h.Voucher);
            return View(histories.ToList());
        }

        // GET: Histories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID");
            return View();
        }

        // POST: Histories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,VoucherID,Money,TimeStamp,HistoryID")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", history.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", history.VoucherID);
            return View(history);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", history.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", history.VoucherID);
            return View(history);
        }

        // POST: Histories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,VoucherID,Money,TimeStamp,HistoryID")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", history.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", history.VoucherID);
            return View(history);
        }

        // GET: Histories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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
