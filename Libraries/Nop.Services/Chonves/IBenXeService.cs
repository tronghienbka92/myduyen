using Nop.Core;
using Nop.Core.Domain.Chonves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Chonves
{
    public partial interface IBenXeService
    {
        PagedList<BenXe> GetAll(int ProvinceId = 0, string tenbenxe = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue);
        List<BenXe> Search(string tenbexe="");
        BenXe GetById(int itemId);
        void Insert(BenXe _item);
        void Update(BenXe _item);
        void Delete(BenXe _item);
        List<BenXe> GetAllBenXe();
        void ProcessDiaDiem();
        PagedList<DiaDiem> GetAllDiaDiem(string tendiadiem = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue);
        DiaDiem GetDiaDiemById(int itemId);
        void InsertDiaDiem(DiaDiem _item);
        void UpdateDiaDiem(DiaDiem _item);
        void DeleteDiaDiem(DiaDiem _item);
    }
}
