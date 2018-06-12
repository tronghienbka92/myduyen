using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    /// <summary>
    /// Class nay dung de index tat ca cac tinh, huyen, ben xe vao mot bang de phuc vu cho viec tim kiem
    /// </summary>
    public class DiaDiem : BaseEntity
    {
        public string Ten { get; set; }
        public int NguonId { get; set; }
        public int LoaiId { get; set; }
        public string TenKhongDau { get; set; }
        public ENLoaiDiaDiem Loai
        {
            get
            {
                return (ENLoaiDiaDiem)LoaiId;
            }
            set
            {
                LoaiId = (int)value;
            }
        }
    }
}
