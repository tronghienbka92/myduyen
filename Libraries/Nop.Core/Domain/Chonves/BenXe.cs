using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;


namespace Nop.Core.Domain.Chonves
{
    public class BenXe : BaseEntity
    {
        public string TenBenXe { get; set; }
        public int DiaChiId { get; set; }
        public int PictureId { get; set; }
        public bool HienThi { get; set; }
        public bool isDelete { get; set; }
        public string MoTa { get; set; }
    }
}
