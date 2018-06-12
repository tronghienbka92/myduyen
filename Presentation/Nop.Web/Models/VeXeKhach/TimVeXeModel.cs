using FluentValidation.Attributes;
using Nop.Core.Domain.Chonves;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.NhaXes;
using Nop.Web.Validators.NhaXes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.VeXeKhach
{
    public class TimVeXeModel
    {

        public TimVeXeModel()
        {
            NgayDi = DateTime.UtcNow.AddHours(12);
            KhuHoi = false;
            DiemKhoiHanh = new DiaDiemModel();
            DiemDen = new DiaDiemModel();
            NhaXes = new List<NguonVeXeModel.NhaXeBasicModel>();
            NguoVeXes = new List<NguonVeXeModel>();
        }
        public DiaDiemModel DiemKhoiHanh { get; set; }
        public DiaDiemModel DiemDen { get; set; }
        [UIHint("DateChonVe")]
        public DateTime NgayDi { get; set; }

        [UIHint("DateChonVe")]
        public DateTime? NgayVe { get; set; }
        public List<NguonVeXeModel.NhaXeBasicModel> NhaXes { get; set; }
        public string NhaXeIds { get; set; }
        public List<NguonVeXeModel> NguoVeXes { get; set; }
        public int KhungGioId { get; set; }
        public string KhungGioIds { get; set; }
        public ENKhungGio KhungGio
        {
            get
            {
                return (ENKhungGio)KhungGioId;
            }
            set
            {
                KhungGioId = (int)value;
            }
        }

        public IList<SelectListItem> khunggios { get; set; }
        public bool KhuHoi { get; set; }
        public int KieuXeId { get; set; }

    }
    public class DiaDiemModel : BaseNopEntityModel
    {
        public int NguonId { get; set; }
        public string Ten { get; set; }
        public int LoaiId { get; set; }
        public string LoaiText { get; set; }
    }
}