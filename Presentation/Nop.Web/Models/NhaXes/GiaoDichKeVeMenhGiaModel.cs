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

    public class GiaoDichKeVeMenhGiaModel : BaseNopEntityModel
    {
        public int STT { get; set; }
        public int MenhGiaId { get; set; }
        public decimal MenhGia { get; set; }
        public IList<SelectListItem> menhgias { get; set; }
        public int SoLuong { get; set; }
        public string SeriFrom { get; set; }
        public string SeriTo { get; set; }
        public string GhiChu { get; set; }
        public bool isVeDi { get; set; }
        public int NguoiNhanId { get; set; }
        public int VanPhongId { get; set; }
        public int LoaiVeId { get; set; }
        public bool isVeMoi { get; set; }
        public string MauVe { get; set; }
        public IList<SelectListItem> mauves { get; set; }
        public string KyHieu { get; set; }
        public int? QuanLyMauVeKyHieuId { get; set; }
        public int ActionTypeId { get; set; }
        public ENGiaoDichKeVeMenhGiaAction ActionType
        {
            get
            {
                return (ENGiaoDichKeVeMenhGiaAction)ActionTypeId;
            }
            set
            {
                ActionTypeId = (int)value;
            }
        }
        public int isNew { get; set; }

    }
}