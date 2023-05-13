using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: Admin/TaiKhoan
        public ActionResult Index()
        {

            return View(db.TaiKhoans.ToList());
        }

        // GET: Admin/TaiKhoan/Details/5
        public ActionResult Details(int id)
        {
            var data = db.TaiKhoans.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "TaiKhoan");
            }
            return View(data);
        }

        // GET: Admin/TaiKhoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TaiKhoan/Create
        [HttpPost]
        public ActionResult Create(TaiKhoan taiKhoans)
        {
            try
            {
                db.TaiKhoans.Add(taiKhoans);
                db.SaveChanges();
                ViewBag.Done = "Thêm tài khoản thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(taiKhoans);
        }

        // GET: Admin/TaiKhoan/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.TaiKhoans.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "TaiKhoan");
            }
            return View(data);
        }

        // POST: Admin/TaiKhoan/Edit/5
        [HttpPost]
        public ActionResult Edit(TaiKhoan taiKhoans, string MatKhauMoi, string MatKhauCu)
        {
            try
            {
                var tk = db.TaiKhoans.Find(taiKhoans.Id);
                if (MatKhauMoi == "" && MatKhauCu == "")
                {
                }
                else
                {
                    if (MatKhauCu != tk.MatKhau || MatKhauMoi == "")
                    {
                        ViewBag.Error = "Mật khẩu không đúng";
                        return View(taiKhoans);
                    }

                }

                tk.HoTen = taiKhoans.HoTen;
                if (MatKhauMoi != "") tk.MatKhau = MatKhauMoi;
                tk.GioiTinh = taiKhoans.GioiTinh;
                tk.Email = taiKhoans.Email;
                tk.DiaChi = taiKhoans.DiaChi;
                tk.TrangThai = taiKhoans.TrangThai;
                tk.Quyen = taiKhoans.Quyen;
                tk.NgaySinh = taiKhoans.NgaySinh;
                db.SaveChanges();
                ViewBag.Done = "Sửa tài khoản thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(taiKhoans);
        }

        // GET: Admin/TaiKhoan/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.TaiKhoans.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "TaiKhoan");
            }
            return View(data);
        }

        // POST: Admin/TaiKhoan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TaiKhoan taiKhoans)
        {
            try
            {
                TaiKhoan dbEntity = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(dbEntity);
                db.SaveChanges();
                TempData["Done"] = "Xóa tài khoản thành công";
                string data = Session["TaiKhoan"].ToString();
                string[] Account = new string[3];
                Account = (data != null) ? data.Split(',') : Account;
                if (id == int.Parse(Account[2]))
                {
                    Session.Remove("TaiKhoan");
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "TaiKhoan");
        }
        public ActionResult Logout()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("Index", "Home");
        }
    }
}
