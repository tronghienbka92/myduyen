using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    public class PhoiVe : BaseEntity
    {
        public PhoiVe()
        {
            IsRequireCancel = false;
        }
        public int? ChuyenDiId { get; set; }
        public virtual HistoryXeXuatBen ChuyenDi { get; set; }
        public int NguonVeXeId { get; set; }
        public int? ChangId { get; set; }
        public virtual HanhTrinhGiaVe changgiave { get; set; }
        public DateTime NgayDi { get; set; }
        public int TrangThaiId { get; set; }
        public int CustomerId { get; set; }
        public int SoDoGheXeQuyTacId { get; set; }
        /// <summary>
        /// Ve dat la cua Chonve.vn hay cua nha xe
        /// </summary>
        public bool isChonVe { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayUpd { get; set; }
        public String SessionId { get; set; }
        public int OrderId { get; set; }
        public int NguonVeXeConId { get; set; }
        public int? SoLanInVe { get; set; }
        public int? NguoiDatVeId { get; set; }
        public bool IsRequireCancel { get; set; }
        public virtual NhanVien nguoidatve { get; set; }
        public ENTrangThaiPhoiVe TrangThai
        {
            get
            {
                return (ENTrangThaiPhoiVe)TrangThaiId;
            }
            set
            {
                TrangThaiId = (int)value;
            }
        }
      

        public virtual Customer customer { get; set; }
        private NguonVeXe _nguonvexe;
        public NguonVeXe getNguonVeXe()
        {
            return nguonvexecon == null ? nguonvexe : nguonvexecon;
        }
        public virtual NguonVeXe nguonvexe
        {
            get
            {               
                return _nguonvexe;
            }
            set
            {
                _nguonvexe = value;
            }
        }
        public virtual NguonVeXe nguonvexecon { get; set; }
        public virtual SoDoGheXeQuyTac sodoghexequytac { get; set; }
        public decimal GiaVeHienTai { get; set; }
        public int? VeXeItemId { get; set; }
        public string GhiChu { get; set; }
        public string MaVe { get; set; }
        public string ViTriLenXe { get; set; }
        public string ViTriXuongXe { get; set; }
        public bool IsForKid { get; set; }
        public Decimal? Latitude { get; set; }
        public Decimal? Longitude { get; set; }
    }
}
