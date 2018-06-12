using Nop.Core;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Services.Chonves
{
    public partial interface IVeXeService
    {
        int GetDiaDiemId(int nguonid, ENLoaiDiaDiem loai);
        DiaDiem GetDiaDiemById(int Id);
        List<DiaDiem> DiaDiemSearch(string keyword = "", int top = 20);
        List<NguonVeXe> VeXeSearch(DateTime NgayDi,
            List<int> NhaXeIds = null,
            int DiemDonId = 0,
            int DiemDenId = 0,
            List<int> KhungGioIds = null,
            int top = 10);
        NguonVeXe GetNguonVeXeById(int Id);
        List<TuyenVeXe> TuyenVeXeSearch(int TinhId, ENKieuXe kieuxe = ENKieuXe.All, int top = 10);      
        TuyenVeXe GetTuyenVeXeById(int Id);
        int GetSoDoGheXeQuyTacID(int LoaiXeId, string KyHieu, int Tang);
    }
}
