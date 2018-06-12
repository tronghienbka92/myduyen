using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ChuyenPhatNhanh
{
    public class PhieuVanChuyen : BaseEntity
    {
        public string SoLenh { get; set; }
        public int SoLenhNum { get; set; }
        public int NhaXeId { get; set; }
        public int VanPhongId { get; set; }
        public virtual VanPhong vanphong { get; set; }
        public int KhuVucDenId { get; set; }
        public virtual KhuVuc KhuVucDen { get; set; }
        public int TrangThaiId { get; set; }
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
        public DateTime NgayTao { get; set; }
        private ICollection<PhieuChuyenPhat> _phieuchuyenphats;
        public virtual ICollection<PhieuChuyenPhat> phieuchuyenphats
        {
            get { return _phieuchuyenphats ?? (_phieuchuyenphats = new List<PhieuChuyenPhat>()); }
            protected set { _phieuchuyenphats = value; }
        }
        private ICollection<PhieuVanChuyenLog> _nhatkyvanchuyens;
        public virtual ICollection<PhieuVanChuyenLog> nhatkyvanchuyens
        {
            get { return _nhatkyvanchuyens ?? (_nhatkyvanchuyens = new List<PhieuVanChuyenLog>()); }
            protected set { _nhatkyvanchuyens = value; }
        }
    }
}
