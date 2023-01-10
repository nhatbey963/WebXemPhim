using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;

namespace WebXemFilm.Controllers
{
    public class LoginController : Controller
    {
        private WebXemPhimEntities db = new WebXemPhimEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var histories = from c in db.Histories select c;
            ViewBag.history = histories.ToList();
            var userr = from s in db.Users select s;
            userr = userr.Where(s => s.Username.CompareTo(username) == 0);
            foreach (var item in userr)
            {
                if (item.Password.CompareTo(password) == 0)
                {
                    var categories = from c in db.Categories select c;
                    ViewBag.categoryID = new SelectList(categories, "CategoryID", "CategoryName");
                    Session["username"] = item.Username;
                    Session["accountname"] = item.Fullname;
                    Session["role"] = item.Status;
                    Session["userid"] = item.ID;
                    ViewBag.error = "Good";
                    if (item.Status == false)
                        return View("~/Views/Home/Index.cshtml");
                    else if (item.Status == true)
                        return View("~/Views/Home/Dashboard.cshtml");
                    else return View("~/Views/Home/Index.cshtml");
                }
            }
            Session["username"] = "";
            ViewBag.error = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
            return View("Index");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("username"); Session.Remove("role"); Session.Remove("userid"); Session.Remove("accountname");
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Fullname,Email,Phone,Address,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.error = "That bai";
            return View(user);
        }
    }
}