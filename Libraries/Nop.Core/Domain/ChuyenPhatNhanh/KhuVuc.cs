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
    public class KhuVuc : BaseEntity
    {
        public string TenKhuVuc { get; set; }
        public string TenVietTat { get; set; }
        public int NhaXeId { get; set; }
        private ICollection<VanPhong> _vanphongs;
        public virtual ICollection<VanPhong> vanphongs
        {
            get { return _vanphongs ?? (_vanphongs = new List<VanPhong>()); }
            protected set { _vanphongs = value; }
        }
    }
}
