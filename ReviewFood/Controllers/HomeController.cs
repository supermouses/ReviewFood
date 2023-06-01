using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Controllers
{
    public class HomeController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        public ActionResult Index()
        {
            var featured = (from tt in db.BaiViets
                            join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
                            //join user in db.TaiKhoans on tt.IdTaiKhoan equals user.Id 
                            where tt.TrangThai == true
                            orderby tt.Id descending
                            select new BaiViet_DanhMuc
                            {
                                DanhMuc = dm.TenDanhMuc,
                                MaDanhMuc = dm.Id,
                                MaTinTuc = tt.Id,
                                TieuDe = tt.TieuDe,
                                NgayTao = tt.NgayTao,
                                HinhAnh = tt.HinhAnh,
                                //IdUser = user.HoTen 
                            }).Take(3);
            ViewBag.Featured = featured;

            ViewBag.TopTen = (from tt in db.BaiViets
                              join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
                              //join user in db.TaiKhoans on tt.IdTaiKhoan equals user.Id 
                              where tt.TrangThai == true
                              orderby tt.Id descending
                              select new BaiViet_DanhMuc
                              {
                                  DanhMuc = dm.TenDanhMuc,
                                  MaDanhMuc = dm.Id,
                                  MaTinTuc = tt.Id,
                                  TieuDe = tt.TieuDe,
                                  NgayTao = tt.NgayTao,
                                  HinhAnh = tt.HinhAnh,
                                  //IdUser = user.HoTen 
                              }).Take(10);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult MenuView()
        {
            var menu = db.DanhMucChas.ToList();
            return PartialView(menu);
        }
    }
}