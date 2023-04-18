using ReviewFood.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Controllers
{
    public class DanhMucController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: DanhMuc
        public ActionResult Index(int page = 1, int id = 0)
        {
            var data = db.DanhMucs.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = data.TenDanhMuc;
            var TinTucs = (from tt in db.BaiViets
                           join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
                           orderby tt.Id descending
                           where tt.IdDanhMuc == id
                           select new BaiViet_DanhMuc
                           {
                               DanhMuc = dm.TenDanhMuc,
                               MaDanhMuc = dm.Id,
                               MaTinTuc = tt.Id,
                               TieuDe = tt.TieuDe,
                               NgayTao = tt.NgayTao,
                               HinhAnh = tt.HinhAnh
                           }).ToPagedList(page, 12);
            return View(TinTucs);
        }
    }
}