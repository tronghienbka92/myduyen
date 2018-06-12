using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Chonves
{
    public class TuyenVeXe : BaseEntity, ISlugSupported
    {
        public TuyenVeXe()
        {
            ToWeb = false;
        }
        public int Province1Id { get; set; }
        public virtual StateProvince Province1 { get; set; }
        public int Province2Id { get; set; }
        public virtual StateProvince Province2 { get; set; }
        public Decimal PriceOld { get; set; }
        public Decimal PriceNew { get; set; }
        public bool HienThi { get; set; }
        public bool ToWeb { get; set; }
        public int ThuTu { get; set; }
        public int SoLuongXem { get; set; }
        public bool KhuyenMai { get; set; }
        public int KieuXeId { get; set; }
        public ENKieuXe KieuXe
        {
            get
            {
                return (ENKieuXe)KieuXeId;
            }
            set
            {
                KieuXeId = (int)value;
            }
        }
        
    }
}
