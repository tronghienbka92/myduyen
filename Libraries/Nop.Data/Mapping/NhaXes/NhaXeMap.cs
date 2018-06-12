using Nop.Core.Domain.NhaXes;

namespace Nop.Data.Mapping.NhaXes
{
    public class NhaXeMap : NopEntityTypeConfiguration<NhaXe>
    {
        public NhaXeMap()
        {
            this.ToTable("CV_NhaXe");
            this.HasKey(c => c.Id);
            this.Property(u => u.MaNhaXe).HasMaxLength(100);
            this.Property(u => u.TenNhaXe).HasMaxLength(1000);
            this.Property(u => u.GioiThieu).HasColumnType("ntext");
            this.Property(u => u.Email).HasMaxLength(1000);
            this.Property(u => u.DienThoai).HasMaxLength(100);
            this.Property(u => u.Fax).HasMaxLength(100);
            this.Property(u => u.HotLine).HasMaxLength(100);
        }
    }
}
