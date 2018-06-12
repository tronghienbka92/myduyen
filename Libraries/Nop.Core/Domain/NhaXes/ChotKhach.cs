using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.NhaXes
{
    public class ChotKhach : BaseEntity
    {
        public int NhaXeId { get; set; }
        public string Ma { get; set; }
        public DateTime NgayChot { get; set; }
        public int NguoiChotId { get; set; }
        public virtual NhanVien nguoichot { get; set; }
        public int HistoryXeXuatBenId { get; set; }
        public virtual HistoryXeXuatBen historychuyenxe { get; set; }
        public int SoLuongPhanMem { get; set; }
        public int SoLuongThucTe { get; set; }
        public int DiemDonId { get; set; }
        public virtual DiemDon diemchot { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public string ViTriChot { get; set; }
    }
}
