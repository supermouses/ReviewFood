using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReviewFood.Models
{
    public class BaiViet_DanhMuc
    {
        public int MaTinTuc { get; set; }
        public int MaDanhMuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string DanhMuc { get; set; }
        public string HinhAnh { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayTao { get; set; }
    }
}