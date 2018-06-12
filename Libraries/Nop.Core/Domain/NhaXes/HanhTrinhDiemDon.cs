using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class HanhTrinhDiemDon : BaseEntity
    {
        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh hanhtrinh { get; set; }
        public int DiemDonId { get; set; }
        public virtual DiemDon diemdon { get; set; }
        public int ThuTu { get; set; }
        public int KhoangCach { get; set; }
        
    }
    public class HanhTrinhDiemChot : BaseEntity
    {
        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh hanhtrinh { get; set; }
        public int DiemDonId { get; set; }
        public virtual DiemDon diemdon { get; set; }
        public int ThuTu { get; set; }
        public int KhoangCach { get; set; }

    }
}
