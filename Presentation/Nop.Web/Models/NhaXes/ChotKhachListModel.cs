using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class ChotKhachListModel
    {
        public ChotKhachListModel()
        {
            diemchots = new List<SelectListItem>();
        }
                 
        public string MaGiaoDich { get; set; }
        public int NguoiChotId { get; set; }
        public int DiemChotId { get; set; }
        public List<SelectListItem>  diemchots { get; set; }

        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
    }
}