using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{

    public class VeXeItem : BaseEntity
    {
        public int MauVeKyHieuId { get; set; }
        public string MauVe { get; set; }
        public string KyHieu { get; set; }
        public string SoSeri { get; set; }
        public bool isVeDi { get; set; }
        public int? NhanVienId { get; set; }
        public int? VanPhongId { get; set; }
        public int? LoaiVeId { get; set; }
        public virtual NhanVien nhanvien { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayNhap { get; set; }
        public DateTime? NgayGiaoVe { get; set; }
        public int? ChangId { get; set; }
        public virtual HanhTrinhGiaVe changgiave { get; set; }
        public DateTime? NgayBan { get; set; }
        public DateTime? NgayDi { get; set; }
        public int? NguonVeXeId { get; set; }
        public virtual NguonVeXe nguonve { get; set; }
        public int TrangThaiId { get; set; }

        public ENVeXeItemTrangThai TrangThai
        {
            get
            {
                return (ENVeXeItemTrangThai)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }
        public int MenhGiaId { get; set; }
        public virtual MenhGiaVe menhgia { get; set; }
        public int NhaXeId { get; set; }
        public bool isDeleted { get; set; }
        public int SoSeriNum { get; set; }
        public bool CanDelete { get; set; }
        public int? XeXuatBenId { get; set; }
        public virtual HistoryXeXuatBen xexuatben {get;set;}
        public int? QuyenId { get; set; }
        public virtual VeXeQuyen quyenve { get; set; }
        public int ThuTuBan { get; set; }
        public int SoQuyen { get; set; }
        public Decimal GiaVe { get; set; }
        public Boolean isGiamGia { get; set; }
    }
}
