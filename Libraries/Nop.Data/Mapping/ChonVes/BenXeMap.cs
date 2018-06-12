using Nop.Core.Domain.Chonves;

namespace Nop.Data.Mapping.ChonVes
{
    public class BenXeMap : NopEntityTypeConfiguration<BenXe>
    {
        public BenXeMap()
        {
            this.ToTable("CV_BenXe");
            this.HasKey(c => c.Id);
            this.Property(u => u.TenBenXe).HasMaxLength(500);
            this.Property(u => u.MoTa).HasColumnType("ntext");
        }
    }
}
