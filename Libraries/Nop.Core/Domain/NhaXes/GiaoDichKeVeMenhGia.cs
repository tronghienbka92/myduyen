using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class GiaoDichKeVeMenhGia : BaseEntity
    {
        public int GiaoDichKeVeId { get; set; }
        public virtual GiaoDichKeVe giaodichkeve { get; set; }
        public bool isVeDi { get; set; }
        public bool isVeMoi { get; set; }
        public int MenhGiaId { get; set; }
        public virtual MenhGiaVe menhgia { get; set; }
        public int SoLuong { get; set; }
        public int? QuanLyMauVeKyHieuId { get; set; }
        public virtual QuanLyMauVeKyHieu quanly { get; set; }
        public string SeriFrom { get; set; }
        public int SeriNumFrom
        {
            get
            {
                int _SeriNumFrom;
                if (int.TryParse(SeriFrom, out _SeriNumFrom))
                    return _SeriNumFrom;
                else
                    return 0;
            }
        }
        public string GhiChu { get; set; }

        private ICollection<GiaoDichKeVeXeItem> _vexeitems;
        public virtual ICollection<GiaoDichKeVeXeItem> vexeitems
        {
            get { return _vexeitems ?? (_vexeitems = new List<GiaoDichKeVeXeItem>()); }
            protected set { _vexeitems = value; }
        }
        public int ActionTypeId { get; set; }
        public ENGiaoDichKeVeMenhGiaAction ActionType
        {
            get
            {
                return (ENGiaoDichKeVeMenhGiaAction)ActionTypeId;
            }
            set
            {
                ActionTypeId = (int)value;
            }
        }
        public int? VanPhongId { get; set; }
        public int LoaiVeId { get; set; }
        public int NguoiNhanId { get; set; }
     
      
    }
}
