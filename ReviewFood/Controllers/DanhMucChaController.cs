using PagedList;
using ReviewFood.Areas.Admin.Controllers;
using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Controllers
{
    public class DanhMucChaController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: DanhMuc
        public ActionResult Index(int page = 1, int id = 0)
        {
            var data = db.DanhMucChas.Find(id);
            var datas = db.DanhMucs.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = data.TenDM;
            var TinTucs = (from tt in db.BaiViets
                           join dmc in db.DanhMucChas on tt.IdDMCha equals dmc.MaDMCha
                           join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
                           orderby tt.Id descending
                           where tt.TrangThai == true
                           where tt.IdDanhMuc == id
                           where dmc.MaDMCha == id
                           select new BaiViet_DanhMucCha
                           {
                               MaDMCha = dmc.MaDMCha,
                               DanhMuc = dm.TenDanhMuc,
                               MaDanhMuc = dm.Id,
                               MaTinTuc = tt.Id,
                               TieuDe = tt.TieuDe,
                               NgayTao = tt.NgayTao,
                               HinhAnh = tt.HinhAnh
                           }).ToPagedList(page, 12);

            //ViewBag.Titles = datas.TenDanhMuc;
            //var TinTucw = (from tt in db.BaiViets
            //               join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
            //               orderby tt.Id descending
            //               where tt.TrangThai == true
            //               where tt.IdDanhMuc == id
            //               select new BaiViet_DanhMuc
            //               {
            //                   DanhMuc = dm.TenDanhMuc,
            //                   MaDanhMuc = dm.Id,
            //                   MaTinTuc = tt.Id,
            //                   TieuDe = tt.TieuDe,
            //                   NgayTao = tt.NgayTao,
            //                   HinhAnh = tt.HinhAnh
            //               }).Take(10);
            return View(TinTucs);
        }
    }
}