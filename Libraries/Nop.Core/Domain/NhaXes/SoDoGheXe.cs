using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class SoDoGheXe : BaseEntity
    {
        public string TenSoDo { get; set; }
        public int SoLuongGhe { get; set; }
        public int KieuXeId { get; set; }
        public int SoCot { get; set; }
        public int SoHang { get; set; }
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
