using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class HistoryXeXuatBen : BaseEntity
    {
        public int NhaXeId { get; set; }
        public int NguonVeId { get; set; }
        public virtual NguonVeXe NguonVeInfo { get; set; }
        public int? XeVanChuyenId { get; set; }
        public virtual XeVanChuyen xevanchuyen { get; set; }
        public DateTime NgayDi { get; set; }
        public int SoNguoi { get; set; }
        public DateTime NgayTao { get; set; }
        public int NguoiTaoId { get; set; }
        public virtual NhanVien NguoiTao { get; set; }
        public string GhiChu { get; set; }

        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh HanhTrinh { get; set; }
       
        public int TrangThaiId { get; set; }
        public ENTrangThaiXeXuatBen TrangThai
        {
            get
            {
                return (ENTrangThaiXeXuatBen)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }
        private ICollection<HistoryXeXuatBenLog> _nhatkys;
        public virtual ICollection<HistoryXeXuatBenLog> NhatKys
        {
            get { return _nhatkys ?? (_nhatkys = new List<HistoryXeXuatBenLog>()); }
            protected set { _nhatkys = value; }
        }
        private ICollection<HistoryXeXuatBen_NhanVien> _laiphuxes;
        public virtual ICollection<HistoryXeXuatBen_NhanVien> LaiPhuXes
        {
            get { return _laiphuxes ?? (_laiphuxes = new List<HistoryXeXuatBen_NhanVien>()); }
            protected set { _laiphuxes = value; }
        }
    }
   
}
