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
    public class PhoiVeModel : BaseNopEntityModel
    {
        public PhoiVeModel()
        {
            LyDoHuy = new LyDoHuyModel();
            ArrLyDoHuy = new List<LyDoHuyModel>(); 
        }
        public int NguonVeXeId { get; set; }
        public Decimal GiaVe { get; set; }
        public string MaVe { get; set; }
        public String GiaVeText { get; set; }

        public DateTime NgayDi { get; set; }
        public int TrangThaiId { get; set; }
        public int SoLuong { get; set; }
        public int CustomerId { get; set; }
        public int SoDoGheXeQuyTacId { get; set; }
        public string TenHanhTrinh { get; set; }
        public string TenLichTrinh { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        public LyDoHuyModel LyDoHuy { get; set; }
        public List<LyDoHuyModel> ArrLyDoHuy { get; set; }
        public string ViTriLenXe { get; set; }
        public string ViTriXuongXe { get; set; }
        public string GhiChu { get; set; }
        public string KyHieuGhe { get; set; }
        public DateTime NgayDisearch { get; set; }
        public int Tang { get; set; }
        /// <summary>
        /// Ve dat la cua Chonve.vn hay cua nha xe
        /// </summary>
        public bool isChonVe { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpd { get; set; }
        public String SessionId { get; set; }

        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiId;
            }            
        }
        public class LyDoHuyModel
        {
            public int Id { get; set; }
            public string LyDo { get; set; }
        }
    }
   
}