using ReviewFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewFood.Areas.Admin.Controllers
{
    public class DanhMucChaController : Controller
    {
        db_WebReviewFoodEntities db = new db_WebReviewFoodEntities();
        // GET: Admin/DanhMucCha
        public ActionResult Index()
        {
            return View(db.DanhMucChas.ToList());
        }


        // GET: Admin/DanhMucCha/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Admin/DanhMucCha/Create
        [HttpPost]
        public ActionResult Create(DanhMucCha danhMucCha)
        {
            try
            {

                db.DanhMucChas.Add(danhMucCha);
                db.SaveChanges();
                ViewBag.Done = "Thêm danh mục thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(danhMucCha);
        }
        // GET: Admin/DanhMucCha/Edit/
        public ActionResult Edit(int id)
        {
            var data = db.DanhMucChas.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "DanhMucCha");
            }
            return View(data);
        }

        // POST: Admin/DanhMuc/Edit/5
        [HttpPost]
        public ActionResult Edit(DanhMucCha danhMucCha)
        {
            try
            {

                var query = (from dm in db.DanhMucChas
                             where dm.MaDMCha == danhMucCha.MaDMCha
                             select dm).Take(1);
                foreach (DanhMucCha dm in query)
                {
                    dm.TenDM = danhMucCha.TenDM;
                };
                db.SaveChanges();
                ViewBag.Done = "Sửa danh mục thành công";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(danhMucCha);
        }



        // GET: Admin/DanhMucCha/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.DanhMucChas.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index", "DanhMucCha");
            }
            return View(data);
        }

        // POST: Admin/DanhMuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DanhMucCha data)
        {
            try
            {
                DanhMucCha dbEntity = db.DanhMucChas.Find(id);
                db.DanhMucChas.Remove(dbEntity);
                db.SaveChanges();
                TempData["Done"] = "Xóa danh mục thành công";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "DanhMucCha");
        }
    }
}