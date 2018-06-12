using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Chonves
{
    public class KhungThoiGian
    {
        public KhungThoiGian(ENKhungGio khunggio)
        {
             GioTu = 0;
            GioDen = 24;
            switch (khunggio)
            {
                case ENKhungGio.Sang:
                    {
                        GioDen = 12;
                    }
                    break;
                case ENKhungGio.Chieu:
                    {
                        GioTu = 12;
                        GioDen = 19;
                    }
                    break;
                case ENKhungGio.Toi:
                    {
                        GioTu = 19;
                        GioDen = 24;
                    }
                    break;
            }
        }
        public int GioTu { get; set; }
        public int GioDen { get; set; }
    }
}
