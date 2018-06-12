using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;

namespace Nop.Data.Mapping.ChuyenPhatNhanh
{
    public class KhachHangMap : NopEntityTypeConfiguration<KhachHang>
    {
        public KhachHangMap()
        {
            this.ToTable("CV_KhachHang");
            this.HasKey(c => c.Id);
           
        }
    }
}
