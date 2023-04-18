using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Areas.Admin.Controllers
{
    public class DanhGiaController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: Admin/DanhGia
        public ActionResult Index(int id)
        {
            object data = (from cmt in db.DanhGias
                           join tt in db.BaiViets on cmt.Id equals tt.Id
                           join tk in db.TaiKhoans on cmt.IdTaiKhoan equals tk.Id
                           where cmt.Id == id
                           select new DanhGia_BaiViet
                           {
                               Id = cmt.Id,
                               NoiDung = cmt.NoiDung,
                               BaiViet = tt.TieuDe,
                               TaiKhoan = tk.HoTen,
                               NgayTao = cmt.NgayTao,
                               NgaySua = cmt.NgaySua,
                               TrangThai = cmt.TrangThai
                           }).ToList();
            ViewBag.Data = data;
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(db.DanhGias.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, DanhGia danhGias)
        {
            var data = db.DanhMucs.Find(id);
            if (data == null)
            {
                return RedirectToAction("/Admin/Index/" + id);
            }
            return View(data);
        }

        public ActionResult Change(int id)
        {
            var data = db.DanhGias.Find(id);
            data.TrangThai = !data.TrangThai;
            data.NgaySua = DateTime.Now;
            db.SaveChanges();
            ViewBag.Done = "Thay đổi thành công";
            return RedirectToAction("/Index/" + data.IdTinTuc);
        }
    }
}