using Nop.Core;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public partial interface IHangHoaService 
    {

        List<HangHoa> GetAllHangHoaByPhieuGuiHangId(int phieuguihangid);

        void InsertHangHoa(HangHoa _item);
        void UpdateHangHoa(HangHoa _item);
        void DeleteHangHoa(HangHoa _item);
        HangHoa GetHangHoaById(int id);
        
        //loai hang hoa
        List<LoaiHangHoa> GetAllLoaiHangHoa(int NhaXeId);

        void Insert(LoaiHangHoa _item);
        void Update(LoaiHangHoa _item);
        void Delete(LoaiHangHoa _item);
        LoaiHangHoa GetLoaiHangHoaById(int id);

        //BangGiaCuoc
        List<BangGiaCuoc> GetAllBangGiaCuoc(int NhaXeId);

        void Insert(BangGiaCuoc _item);
        void Update(BangGiaCuoc _item);
        void Delete(BangGiaCuoc _item);
        BangGiaCuoc GetBangGiaCuocById(int id);
    }
}
