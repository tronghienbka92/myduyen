using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class ChuyenDiTaiChinh : BaseEntity
    {
        public ChuyenDiTaiChinh()
        {
            NgayTao = DateTime.Now;
           
            isCapNhat = false;
        }
        public int NhaXeId { get; set; }
        public int? XeVanChuyenId { get; set; }
        public virtual XeVanChuyen xevanchuyen { get; set; }
        public int NguoiTaoId { get; set; }
        public virtual NhanVien nguoitao { get; set; }
        public DateTime NgayTao { get; set; }
        public bool isCP1 { get; set; }
        public int LuotDiId { get; set; }
        public virtual HistoryXeXuatBen luotdi { get; set; }
        public int? LuotVeId { get; set; }
        public virtual HistoryXeXuatBen luotve { get; set; }

        private ICollection<ChuyenDiTaiChinhThuChi> _giaodichthuchis;
        public virtual ICollection<ChuyenDiTaiChinhThuChi> GiaoDichThuChis
        {
            get { return _giaodichthuchis ?? (_giaodichthuchis = new List<ChuyenDiTaiChinhThuChi>()); }
            protected set { _giaodichthuchis = value; }
        }
        public int? ChuyenDiTCLaiXeId { get; set; }
        public virtual ChuyenDiTaiChinh ChuyenDiTCLaiXe { get; set; }
        public int? ChuyenDiTCPhuXeId { get; set; }
        public virtual ChuyenDiTaiChinh ChuyenDiTCPhuXe { get; set; }
        public bool isCapNhat { get; set; }
        public decimal ThucThu { get; set; }
        public decimal DinhMucDau { get; set; }
        public decimal ThucDo { get; set; }
        public decimal GiaDau { get; set; }
        public decimal VeQuay { get; set; }
        //add by lent 15-12-2015
        //them thong tin nguoi thu, nguoi nop, ngay giao dich

        public int? NguoiThuId { get; set; }
        public virtual NhanVien nguoithu { get; set; }
        public int? NguoiNopId { get; set; }
        public virtual NhanVien nguoinop { get; set; }
        public DateTime? NgayGiaoDich { get; set; }
      
    }
}
