using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
namespace Nop.Data.Mapping.NhaXes
{
    public class NhaXeCustomerMap : NopEntityTypeConfiguration<NhaXeCustomer>
    {
        public NhaXeCustomerMap()
        {
            this.ToTable("CV_NhaXeCustomer");
            this.HasKey(c => c.Id);
            this.Property(u => u.HoTen).HasMaxLength(200);
            this.Property(u => u.DienThoai).HasMaxLength(50);
            this.Property(u => u.SearchInfo).HasMaxLength(500);

        }

    }
}
