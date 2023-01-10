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
    public class HomeController : Controller
    {
        private WebXemPhimEntities db = new WebXemPhimEntities();
        public ActionResult Index()
        {
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }
        public ActionResult Dashboard()
        {
            var histories = from c in db.Histories select c;
            ViewBag.history = histories.ToList();
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }
        public ActionResult Payment()
        {
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }
        public ActionResult Payment2()
        {

            var vouchers = from c in db.Vouchers select c;
            ViewBag.voucher = vouchers.ToList();
            ViewBag.voucherID = new SelectList(db.Vouchers, "VoucherID", "Num_Discount");
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }
        [HttpPost]
        public ActionResult ToPays(int Package = 0, int Month = 0, int voucherid = 0)
        {
            // Nhận giohang từ View truyền sang
            History history = new History();
            float money = 10000;
            if (Package == 2) money = 20000;
            else if (Package == 3) money = 30000;
            int historyid = 1;

            while(true)
            {
                var tmp = db.Histories.FirstOrDefault(x => x.HistoryID == historyid);
                if (tmp == null)  break;
                historyid++;
            }
            history.HistoryID = historyid;
            history.Money = Month * money;
            history.TimeStamp = DateTime.Now;
            history.UserID = int.Parse(Session["userid"].ToString());
            history.VoucherID = voucherid;
            if (voucherid != -1)
            {
                Voucher voucher = db.Vouchers.FirstOrDefault(x => x.VoucherID == voucherid);
                if (voucher.Amount > 0)
                {
                    // history.VoucherID = voucherid;
                    history.Money -= voucher.Num_Discount;
                    voucher.Amount = voucher.Amount - 1;
                    db.Entry(voucher).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //Voucher voucher = db.Vouchers.FirstOrDefault(x => x.VoucherID == voucherid);
            }
            //history.VoucherID = voucherid;
            db.Histories.Add(history);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (Session["username"] != "" && Session["username"] != null) return View();
            return View("~/Views/Login/Index.cshtml");
        }
    }
}