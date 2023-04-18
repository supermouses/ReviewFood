using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Controllers
{
    public class TaiKhoanController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: TaiKhoan
        public ActionResult Login()
        {
            if (Session["TaiKhoan"] == null)
                return View();
            else
                return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(string TenDangNhap, string MatKhau)
        {
            var data = db.TaiKhoans.Where(tk => tk.TenDangNhap == TenDangNhap && tk.MatKhau == MatKhau).FirstOrDefault();
            //if (data == null)
            //{
            //    ViewBag.Error = "Thông tin đăng nhập không đúng";
            //    return View();
            //}
            //else
            //{
            //    string data_account = data.TenDangNhap + "," + data.HoTen + "," + data.Id;
            //    //Session["TaiKhoan"] = data;
            //    Session.Add("TaiKhoan", data_account);
            //    return Redirect("/");
            //}
            if (data.Quyen == true)
            {
                Session["TaiKhoan"] = data;
                return RedirectToAction("Index", "Admin/TaiKhoan");
            }
            else
            {
                Session["TaiKhoan"] = data;
                return RedirectToAction("Index", "Home");
            }
        }
            public ActionResult Logout()
        {
            Session.Remove("TaiKhoan");
            return Redirect("/");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.HoTen = collection["HoTen"];
                taiKhoan.GioiTinh = bool.Parse(collection["GioiTinh"]);
                taiKhoan.TenDangNhap = collection["TenDangNhap"];
                taiKhoan.MatKhau = collection["MatKhau"];
                taiKhoan.Email = collection["Email"];
                taiKhoan.DiaChi = collection["DiaChi"];
                taiKhoan.TrangThai = true;
                taiKhoan.Quyen = false;
                taiKhoan.NgaySinh = DateTime.Parse(collection["NgaySinh"]);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                ViewBag.Done = "Đăng ký thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaiKhoan"] == null)
            {
                return Redirect("/");
            }
            else
            {
                string data = Session["TaiKhoan"].ToString();
                string[] Account = new string[3];
                Account = (data != null) ? data.Split(',') : Account;
                if (int.Parse(Account[2]) != id)
                {
                    return Redirect("/");
                }
            }
            return View(db.TaiKhoans.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(TaiKhoan taiKhoans)
        {
            try
            {
                var tk = db.TaiKhoans.Find(taiKhoans.Id);
                tk.HoTen = taiKhoans.HoTen;
                tk.MatKhau = taiKhoans.MatKhau;
                tk.GioiTinh = taiKhoans.GioiTinh;
                tk.Email = taiKhoans.Email;
                tk.DiaChi = taiKhoans.DiaChi;
                tk.NgaySinh = taiKhoans.NgaySinh;
                db.SaveChanges();
                ViewBag.Done = "Sửa tin tức thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(taiKhoans);
        }
    }
}