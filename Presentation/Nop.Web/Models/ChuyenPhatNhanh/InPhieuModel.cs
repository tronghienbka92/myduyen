using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    public class ListPhieuModel : BaseNopEntityModel
    {

        public ListPhieuModel()
        {            
           
            isXepPhieu = false;
            isCoCuocTanNoi = false;
            VanPhongs = new List<SelectListItem>();
        }
        public string TenVanPhongHienTai { get; set; }
        public string MaPhieu { get; set; }
        public string TenNguoiGui { get; set; }
        public string HangHoaInfo { get; set; }
        public string ThongTinChuyen { get; set; }
        public string TenNguoiNhan { get; set; }
        [UIHint("DateNgayTaoPhieuHang")]
        public DateTime? NgayTao { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        public int VanPhongNhanId { get; set; }
        public int VanPhongGuiId { get; set; }
        public int XeXuatBenId { get; set; }
        public int PhieuVanChuyenId { get; set; }
        public int OutPhieuVanChuyenId { get; set; }
        public int TrangThaiId { get; set; }
        public string TrangThaiIds { get; set; }
        public IList<SelectListItem> trangthais { get; set; }
        public ENTrangThaiChuyenPhat TrangThai
        {
            get
            {
                return (ENTrangThaiChuyenPhat)this.TrangThaiId;
            }
            set
            {
                this.TrangThaiId = (int)value;
            }
        }
        public string SoLenh { get; set; }
        public int TrangThaiVanChuyenId { get; set; }
        public string TrangThaiVanChuyenIds { get; set; }
        public ENTrangThaiPhieuVanChuyen TrangThaiVanChuyen
        {
            get
            {
                return (ENTrangThaiPhieuVanChuyen)this.TrangThaiVanChuyenId;
            }
            set
            {
                this.TrangThaiVanChuyenId = (int)value;
            }
        }
      
        public IList<SelectListItem> VanPhongs { get; set; }
        public IList<SelectListItem> trangthaivanchuyens { get; set; }
        public bool isTraHang { get; set; }
        public IList<SelectListItem> phieuvanchuyens { get; set; }
        /// <summary>
        /// isXepPhieu=true sap xep phieu bien nhan theo vung id, id desc
        /// isXepPhieu=false sap xep phieu bien nhan id desc
        /// </summary>
        public bool isXepPhieu { get; set; }
        public bool isCoCuocTanNoi { get; set; }
        public bool isHangTon { get; set; }

    }
}
