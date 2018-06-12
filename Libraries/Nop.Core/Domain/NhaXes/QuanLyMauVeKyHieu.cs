using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.NhaXes
{
    public class QuanLyMauVeKyHieu : BaseEntity
    {
        public QuanLyMauVeKyHieu()
        {
            isMacDinh = true;
        }
        public string MauVe { get; set; }
        public string KyHieu { get; set; }
        public int NhaXeId { get; set; }
        public bool isMacDinh { get; set; }
    }
}
