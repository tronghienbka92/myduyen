using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class XeVanChuyen:BaseEntity
    {
        public int NhaXeId { get; set; }
        public string TenXe { get; set; }
        public int LoaiXeId { get; set; }
        public virtual LoaiXe loaixe { get; set; }
        public string BienSo { get; set; }
        public string DienThoai { get; set; }
        public int TrangThaiXeId { get; set; }

        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }

        public DateTime? NgayGPS { get; set; }
        public ENTrangThaiXe TrangThaiXe
        {
            get
            {
                return (ENTrangThaiXe)TrangThaiXeId;
            }
            set
            {
                TrangThaiXeId = (int)value;
            }
        }
        public DateTime? NgayDangKiem { get; set; }
        public string PhuHieuXe { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public string BaoHiem { get; set; }
    }
}
