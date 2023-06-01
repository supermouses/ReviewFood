using Microsoft.Ajax.Utilities;
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
            int TongDanhMucCha = db.DanhMucChas.Count();
            int TongDanhMuc = db.DanhMucs.Count();
            int TongTinTuc = db.BaiViets.Count();
            int TongTinChuaDuyet = db.BaiViets.Where(tt => tt.TrangThai == false).Count();
            int TaiKhoanChuaDuyet = db.TaiKhoans.Where(ttk => ttk.TrangThai == false).Count();
            int TongTaiKhoan = db.TaiKhoans.Count();  
            int[] data = new int[] { TongDanhMucCha, TongDanhMuc, TongTinTuc, TongTaiKhoan, TongTinChuaDuyet, TaiKhoanChuaDuyet };
            ViewBag.Data = data;
            return View();
        }
    }
}