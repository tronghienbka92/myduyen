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
    public class QLThuChiModel
    {
        public QLThuChiModel()
        {
            TuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DenNgay = DateTime.Now.Date;
            isChi = -1;
            ddlThuChi = new List<SelectListItem>();
            ddlThuChi.Add(new SelectListItem
            {
                Value="-1",
                Text = "-----Tất cả-----",
                Selected=true
            });
            ddlThuChi.Add(new SelectListItem
            {
                Value = "0",
                Text = "Thu",
                Selected = true
            });
            ddlThuChi.Add(new SelectListItem
            {
                Value = "1",
                Text = "Chi",
                Selected = true
            });

        }
        public int LoaiThuChiId { get; set; }
        public List<SelectListItem> loaithuchis { get; set; }
        [UIHint("DateNullable")]
        public DateTime TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime DenNgay { get; set; }
        public int isChi { get; set; }
        public List<SelectListItem> ddlThuChi { get; set; }
        public String KeySearch { get; set; }
        public List<ThuChiModel> thuchis { get; set; }
        public decimal TonDauKy { get; set; }
    }
}