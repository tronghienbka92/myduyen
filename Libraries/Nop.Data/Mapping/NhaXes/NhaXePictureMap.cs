
using Nop.Data.Mapping;
namespace Nop.Core.Domain.NhaXes
{
    public partial class NhaXePictureMap : NopEntityTypeConfiguration<NhaXePicture>
    {
        public NhaXePictureMap()
        {
            this.ToTable("CV_NhaXeInfo_Picture_Mapping");
            this.HasKey(pp => pp.Id);            
        }
    }
}