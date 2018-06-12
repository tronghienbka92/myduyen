using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class HanhTrinhGiaVe : BaseEntity
    {
        public int HanhTrinhId { get; set; }
        public int NhaXeId { get; set; }
        public int DiemDonId { get; set; }
        public int DiemDenId { get; set; }
        public int? LoaiXeId { get; set; }
        public Decimal GiaVe { get; set; }
        public bool IsDelete { get; set; }
        public virtual HanhTrinh HanhTrinh { get; set; }
        public virtual LoaiXe loaixe { get; set; }
        public virtual DiemDon DiemDon { get; set; }
        public virtual DiemDon DiemDen { get; set; }
        
    }
}
