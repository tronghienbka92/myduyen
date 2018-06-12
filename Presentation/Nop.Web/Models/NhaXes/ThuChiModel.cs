using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class ThuChiModel : BaseNopEntityModel
    {
        public ThuChiModel()
        {
            loaithuchis = new List<SelectListItem>();
            NgayGiaoDich = DateTime.Now.Date;
        }
        public string Ma { get; set; }
        public DateTime NgayTao { get; set; }
        [UIHint("DateNullable")]
        public DateTime NgayGiaoDich { get; set; }
        public DateTime NgayDi { get; set; }
        public int ChuyenDiId { get; set; }
        public string thongtinchuyendi { get; set; }
        public int LoaiThuChiId { get; set; }
        public List<SelectListItem> loaithuchis { get; set; }
        public string tenloaithuchi { get; set; }
        public string DienGiai { get; set; }
        public decimal GiaTri { get; set; }
        public int NguoiNopId { get; set; }
        public string tennguoinop { get; set; }
        public int NguoiThuId { get; set; }
        public string tennguoithu { get; set; }
        public int NguoiTaoId { get; set; }
        public string tennguoitao { get; set; }
        public bool isChi { get; set; }
        public string isChiText { get; set; }
        public int NhaXeId { get; set; }
    }
}