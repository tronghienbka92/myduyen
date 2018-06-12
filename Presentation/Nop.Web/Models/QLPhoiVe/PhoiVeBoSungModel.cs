using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Models.QLPhoiVe
{
    public class PhoiVeBoSungModel
    {
        public int ChuyenDiId { get; set; }
        public int HanhTrinhId { get; set; }
        public int ChangId { get; set; }
        public string TenChang { get; set; }
        public IList<SelectListItem> changs { get; set; }

        public int MauVeId { get; set; }
        public string TenMau { get; set; }
        public IList<SelectListItem> maukyhieus { get; set; }

        public int NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
        public IList<SelectListItem> nhanviens { get; set; }
        public int SoQuyen { get; set; }
        public int SoLuong { get; set; }
        public string SeriFrom { get; set; }
        public string SeriTo { get; set; }
        public string SeriGiamGia { get; set; }

    }
}