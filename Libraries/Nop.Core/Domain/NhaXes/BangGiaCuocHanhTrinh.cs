using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class BangGiaCuocHanhTrinh : BaseEntity
    {
        public int HanhTrinhId { get; set; }
        public virtual HanhTrinh hanhtrinh { get; set; }
        public int BangGiaCuocId { get; set; }
        public virtual BangGiaCuoc banggiacuoc { get; set; }
        public int? DiemDonId1 { get; set; }
        public virtual DiemDon diemdon1 { get; set; }
        public int? DiemDonId2 { get; set; }
        public virtual DiemDon diemdon2 { get; set; }
        public Decimal GiaCuoc { get; set; }
        public Decimal GiaCuocMoRong { get; set; }
    }
}
