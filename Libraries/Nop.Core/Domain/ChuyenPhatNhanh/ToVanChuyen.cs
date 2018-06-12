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
    public class ToVanChuyen : BaseEntity
    {
        public string TenTo { get; set; }
        public string MoTa { get; set; }
        public int NhaXeId { get; set; }
        private ICollection<NguoiVanChuyen> _nguoivanchuyens;
        public virtual ICollection<NguoiVanChuyen> nguoivanchuyens
        {
            get { return _nguoivanchuyens ?? (_nguoivanchuyens = new List<NguoiVanChuyen>()); }
            protected set { _nguoivanchuyens = value; }
        }
        private ICollection<ToVanChuyenVanPhong> _tovanchuyenvps;
        public virtual ICollection<ToVanChuyenVanPhong> tovanchuyenvps
        {
            get { return _tovanchuyenvps ?? (_tovanchuyenvps = new List<ToVanChuyenVanPhong>()); }
            protected set { _tovanchuyenvps = value; }
        }
      
    }
}
