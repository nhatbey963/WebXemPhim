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
    public class UserVouchersController : Controller
    {
        private WebXemPhimEntities db = new WebXemPhimEntities();

        // GET: UserVouchers
        public ActionResult Index()
        {
            var userVouchers = db.UserVouchers.Include(u => u.User).Include(u => u.Voucher);
            return View(userVouchers.ToList());
        }

        // GET: UserVouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVoucher userVoucher = db.UserVouchers.Find(id);
            if (userVoucher == null)
            {
                return HttpNotFound();
            }
            return View(userVoucher);
        }

        // GET: UserVouchers/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID");
            return View();
        }

        // POST: UserVouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoucherID,UserID,Status,UserVoucherID")] UserVoucher userVoucher)
        {
            if (ModelState.IsValid)
            {
                db.UserVouchers.Add(userVoucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", userVoucher.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", userVoucher.VoucherID);
            return View(userVoucher);
        }

        // GET: UserVouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVoucher userVoucher = db.UserVouchers.Find(id);
            if (userVoucher == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", userVoucher.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", userVoucher.VoucherID);
            return View(userVoucher);
        }

        // POST: UserVouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoucherID,UserID,Status,UserVoucherID")] UserVoucher userVoucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userVoucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", userVoucher.UserID);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "VoucherID", userVoucher.VoucherID);
            return View(userVoucher);
        }

        // GET: UserVouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserVoucher userVoucher = db.UserVouchers.Find(id);
            if (userVoucher == null)
            {
                return HttpNotFound();
            }
            return View(userVoucher);
        }

        // POST: UserVouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserVoucher userVoucher = db.UserVouchers.Find(id);
            db.UserVouchers.Remove(userVoucher);
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
