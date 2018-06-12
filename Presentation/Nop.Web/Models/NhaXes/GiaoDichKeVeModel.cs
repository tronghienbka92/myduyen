using FluentValidation.Attributes;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.NhaXes
{
    public class GiaoDichKeVeModel : BaseNopEntityModel
    {
        public GiaoDichKeVeModel()
        {
            nguoinhans = new List<SelectListItem>();
            VanPhongs = new List<SelectListItem>();
            ddlLoaiVes = new List<SelectListItem>();
            isEdit = false;
        }
        public string PhanLoaiText { get; set; }
        public int PhanLoaiId { get; set; }
        public string Ma { get; set; }
        [UIHint("DateNullable")]
        public DateTime NgayKe { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public int NguoiGiaoId { get; set; }
        public string tennguoigiao { get; set; }
        public int NguoiNhanId { get; set; }
        public int VanPhongId { get; set; }
        public String VanPhongText { get; set; }
        public int LoaiVeId { get; set; }
        public ENLoaiVeXeItem LoaiVe
        {
            get
            {
                return (ENLoaiVeXeItem)LoaiVeId;
            }
            set
            {
                LoaiVeId = (int)value;
            }
        }
        public String LoaiVeText { get; set; }
        public string tennguoinhan { get; set; }
        public IList<SelectListItem> ddlLoaiVes { get; set; }
        public IList<SelectListItem> nguoinhans { get; set; }
        public List<GiaoDichKeVeMenhGiaModel> veluotdi { get; set; }
        public List<GiaoDichKeVeMenhGiaModel> veluotve { get; set; }
        public IList<SelectListItem> VanPhongs { get; set; }
        public int TrangThaiId { get; set; }
        public String TrangThaiText { get; set; }
        public ENGiaoDichKeVeTrangThai TrangThai
        {
            get
            {
                return (ENGiaoDichKeVeTrangThai)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }

        public string SessionId { get; set; }
        public bool isVeMoi { get; set; }
        public int NhaXeId { get; set; }
        public string TenNhaXe { get; set; }
        public bool isEdit { get; set; }

    }
}