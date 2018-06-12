using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.NhaXes
{
    public class GheItem:BaseEntity
    {
        public int LoaiXeId { get; set; }
        public string KyHieuGhe { get; set; }
        public int Tang { get; set; }
        public int SoDoGheXeViTriId { get; set; }
        
    }
}
