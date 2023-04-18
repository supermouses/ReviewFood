using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            int TongDanhMuc = db.DanhMucs.Count();
            int TongTinTuc = db.BaiViets.Count();
            int TongTaiKhoan = db.TaiKhoans.Count();
            int[] data = new int[] { TongDanhMuc, TongTinTuc, TongTaiKhoan };
            ViewBag.Data = data;
            return View();
        }
    }
}