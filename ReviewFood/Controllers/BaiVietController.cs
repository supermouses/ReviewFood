using ReviewFood.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ReviewFood.Controllers
{
    public class BaiVietController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: BaiViet
        public ActionResult Index(int id = 0)
        {
            if (TempData["Done"] != null || TempData["Error"] != null)
            {
                ViewBag.Done = TempData["Done"];
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Done");
                TempData.Remove("Error");
            }
            var data = db.BaiViets.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home");
            }
            object comments = (from cmt in db.DanhGias
                               join tt in db.BaiViets on cmt.IdTinTuc equals tt.Id
                               join tk in db.TaiKhoans on cmt.IdTaiKhoan equals tk.Id
                               where cmt.IdTinTuc == id && cmt.TrangThai == true
                               orderby cmt.NgayTao descending
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
            ViewBag.Comments = comments;
            return View(data);
        }

        public ActionResult Search(string keywork = "", int page = 1)
        {
            var TinTucs = (from tt in db.BaiViets
                           join dm in db.DanhMucs on tt.IdDanhMuc equals dm.Id
                           orderby tt.Id descending
                           where tt.TieuDe.Contains(keywork) || tt.NoiDung.Contains(keywork)
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
        public ActionResult Create()
        {
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View();
        }

        // POST: Admin/BaiViet/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiViet baiViet, HttpPostedFileBase img)
        {
            try
            {
                string filename = "";
                if (img != null)
                {
                    filename = Path.GetFileNameWithoutExtension(img.FileName);
                }

                else
                {
                    filename = "";
                }
                if (filename != "")
                {
                    string extension = Path.GetExtension(img.FileName);
                    if (extensionFile(extension) == true)
                    {
                        string fileId = Guid.NewGuid().ToString().Replace("-", "");
                        // tring userId = GetUserId(); Function to get user id based on your schema



                        //file.SaveAs(path);
                        var _filename = filename + extension;
                        filename = filename + extension;
                        filename = Path.Combine(Server.MapPath("~/Assets/Image"), filename);
                        img.SaveAs(filename);
                        baiViet.HinhAnh = _filename;
                    }
                    else
                    {
                        ViewBag.Error = "File không đúng định dạng, hãy điền lại dữ liệu";
                        return View(baiViet);
                    }
                }
                baiViet.TrangThai = false;
                baiViet.NgayTao = DateTime.Now;
                baiViet.NgaySua = DateTime.Now;
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                ViewBag.Done = "Thêm bài viết thành công, xin chờ duyệt";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View(baiViet);
        }
        public bool extensionFile(string extension)
        {
            var st_Exten = extension.ToLower();
            switch (st_Exten)
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".veg":
                    return true;
                default:
                    return false;
            }
        }
    }
}