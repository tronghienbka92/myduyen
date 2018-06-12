using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class NhaXeCauHinh : BaseEntity
    {
        public int NhaXeId { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public ENNhaXeCauHinh MaCauHinh
        {
            get
            {
                return (ENNhaXeCauHinh)Enum.Parse(typeof(ENNhaXeCauHinh),this.Ma);
            }
            set
            {
                Ma = value.ToString();
            }
        }
        public string GiaTri { get; set; }
        public int KieuDuLieuId { get; set; }
        public ENKieuDuLieu kieudulieu 
        { 
            get {
                return (ENKieuDuLieu)KieuDuLieuId;
            } 
            set
            {
                KieuDuLieuId = (int)value;
            }
        }
    }
}
