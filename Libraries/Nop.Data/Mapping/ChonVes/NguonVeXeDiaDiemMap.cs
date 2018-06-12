using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Data.Mapping.ChonVes
{
    public class NguonVeXeDiaDiemMap : NopEntityTypeConfiguration<NguonVeXeDiaDiem>
    {
        public NguonVeXeDiaDiemMap()
        {
            this.ToTable("CV_NguonVeXeDiaDiem");
            this.HasKey(c => c.Id);
        }
    }
}
