using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Web.Models.ChuyenPhatNhanh
{
    public class PhieuVanChuyenModel : BaseNopEntityModel
    {
        public PhieuVanChuyenModel()
        {
            phieuchuyenphats = new List<PhieuChuyenPhatModel>();
            nhatkyvanchuyens = new List<PhieuVanChuyenLogModel>();
            LoaiPhieuVanChuyen = ENLoaiPhieuVanChuyen.TrongTuyen;
            NhatKyVanChuyenHienTai = new PhieuVanChuyenLogModel();
        }
        public string SoLenh { get; set; }
        public int NhaXeId { get; set; }
        public int VanPhongId { get; set; }
        public string TenVanPhong { get; set; }
        public string MaVanPhong { get; set; }
        public int KhuVucDenId { get; set; }
        List<SelectListItem> khuvucs { get; set; }
        public string KhuVucDenText { get; set; }
        public IList<SelectListItem> khuvucdens { get; set; }
        public string TenChang { get; set; }
        public int TrangThaiId { get; set; }
        public string TrangThaiText { get; set; }
        public ENTrangThaiPhieuVanChuyen TrangThai
        {
            get
            {
                return (ENTrangThaiPhieuVanChuyen)this.TrangThaiId;
            }
            set
            {
                this.TrangThaiId = (int)value;
            }
        }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayDi { get; set; }
        public int LoaiPhieuVanChuyenId { get; set; }
        public ENLoaiPhieuVanChuyen LoaiPhieuVanChuyen
        {
            get
            {
                return (ENLoaiPhieuVanChuyen)this.LoaiPhieuVanChuyenId;
            }
            set
            {
                this.LoaiPhieuVanChuyenId = (int)value;
            }
        }
        public string LoaiPhieuVanChuyenText { get; set; }
        public IList<SelectListItem> loaiphieus { get; set; }
        public decimal TongCuocDiKem { get; set; }

        List<PhieuChuyenPhatModel> phieuchuyenphats { get; set; }
        public List<PhieuVanChuyenLogModel> nhatkyvanchuyens { get; set; }

        //de tao thong tin nhat ky chuyen di
        public PhieuVanChuyenLogModel NhatKyVanChuyenHienTai { get; set; }
        public List<SelectListItem> chuyendis { get; set; }
        public List<SelectListItem> vanphongnhans { get; set; }
        //thong tin phieu bien nhan ids
        public string phieuchuyenphatids { get; set; }
        public class PhieuVanChuyenLogModel : BaseNopEntityModel
        {
            public int PhieuVanChuyenId { get; set; }
            public int ChuyenDiId { get; set; }
            public string BienSo { get; set; }
            public string LaiXe { get; set; }
            public DateTime NgayDi { get; set; }
            public string NgayDiText { get; set; }
            public int HanhTrinhId { get; set; }
            public string hanhtrinhText { get; set; }
            public int TuyenId { get; set; }
            public string tuyenText { get; set; }
            public int VanPhongGuiId { get; set; }
            public string vanphongguiText { get; set; }
            public int VanPhongNhanId { get; set; }
            public string vanphongnhanText { get; set; }
            public int KhuVucId { get; set; }
            public string khuvucText { get; set; }
            public decimal TongCuoc { get; set; }
            public int NguoiGiaoId { get; set; }
            public string NguoiGiaoText { get; set; }
            public int NguoiNhanId { get; set; }
            public string NguoiNhanText { get; set; }
            public DateTime NgayTao { get; set; }
            public string GhiChu { get; set; }
        }

    }
}
