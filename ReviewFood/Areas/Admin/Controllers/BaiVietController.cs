using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Areas.Admin.Controllers
{
    public class BaiVietController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: Admin/BaiViet
        public ActionResult Index(string IdDanhMuc, string keyword)
        {
            int idDM = 0;
            if (IdDanhMuc != null) idDM = int.Parse(IdDanhMuc);
            ViewBag.keyword = keyword;
            if (IdDanhMuc != null)
                ViewBag.IdDanhMuc = int.Parse(IdDanhMuc);
            else ViewBag.IdDanhMuc = IdDanhMuc;
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            List<BaiViet> baiViets = new List<BaiViet>();
            if (idDM == 0 && keyword == "" || idDM == 0 && keyword == null)
                baiViets = db.BaiViets.ToList();
            else if (idDM != 0 && keyword == "")
                baiViets = db.BaiViets.Where(dm => dm.IdDanhMuc.Equals(idDM)).ToList();
            else if (idDM == 0 && keyword != "")
                baiViets = db.BaiViets.Where(dm => dm.TieuDe.Contains(keyword)).ToList();
            else
                baiViets = db.BaiViets.Where(dm => dm.TieuDe.Contains(keyword) && dm.IdDanhMuc.Equals(idDM)).ToList();
            if (TempData["Done"] != null || TempData["Error"] != null)
            {
                ViewBag.Done = TempData["Done"];
                ViewBag.Error = TempData["Error"];
                TempData.Remove("Done");
                TempData.Remove("Error");
            }
            return View(baiViets);
        }

        // GET: Admin/BaiViet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/BaiViet/Create
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
                baiViet.NgayTao = DateTime.Now;
                baiViet.NgaySua = DateTime.Now;
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                ViewBag.Done = "Thêm danh mục thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View(baiViet);
        }

        // GET: Admin/TinTuc/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.BaiViets.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "BaiViet");
            }
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View(data);
        }

        // POST: Admin/TinTuc/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaiViet tinTucs, HttpPostedFileBase img)
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
                        var _filename = filename + extension;
                        filename = filename + extension;
                        var httpPostedFile = HttpContext.Request.Form["img"];
                        var t = Path.GetFileName(Request.Form["img"] + "salsda");
                        filename = Path.Combine(Server.MapPath("~/Assets/Image"), filename);
                        img.SaveAs(filename);
                        tinTucs.HinhAnh = _filename;
                    }
                    else
                    {
                        TempData["messageError"] = "File không đúng định dạng, hãy điền lại dữ liệu";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            var query = (from tt in db.BaiViets
                         where tt.Id == tinTucs.Id
                         select tt).Take(1);
            foreach (BaiViet tt in query)
            {
                tt.TieuDe = tinTucs.TieuDe;
                tt.NoiDung = tinTucs.NoiDung;
                if (tinTucs.HinhAnh != null)
                    tt.HinhAnh = tinTucs.HinhAnh;
                else tinTucs.HinhAnh = tt.HinhAnh;
                tt.IdDanhMuc = tinTucs.IdDanhMuc;
                tt.NgaySua = DateTime.Now;
                tt.TrangThai = tinTucs.TrangThai;
            };
            db.SaveChanges();
            ViewBag.Done = "Sửa bài viết thành công";
            ViewBag.DanhMucs = db.DanhMucs.ToList();
            return View(tinTucs);
        }

        // GET: Admin/TinTuc/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.BaiViets.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "DanhMuc");
            }
            return View(data);
        }

        // POST: Admin/TinTuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BaiViet baiviet)
        {
            try
            {
                BaiViet dbEntity = db.BaiViets.Find(id);
                db.BaiViets.Remove(dbEntity);
                db.SaveChanges();
                TempData["Done"] = "Xóa danh mục thành công";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "BaiViet");
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