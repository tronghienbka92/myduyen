using System;
using System.Collections.Generic;

namespace Nop.Core.Domain.NhaXes
{
    public class LichTrinhGiaVe : BaseEntity
    {
        public int LichTrinhID { get; set; }
        public int DiemDon1_Id { get; set; }
        public int DiemDon2_Id { get; set; }
        public Decimal GiaVe { get; set; }
        
    }
}
