using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Chonves;
using System;

namespace Nop.Services.NhaXes
{
    public partial interface IBaoCaoService
    {
        #region Xe xuat ben
        List<HistoryXeXuatBen> GetXeXuatBens(int NhaXeId, int XeVanChuyenId = 0, int[] hanhtrinhIds=null, int[] laiphuxeids = null, DateTime? TuNgay = null, DateTime? DenNgay = null);
        #endregion
        #region Phoi ve
        Decimal GetTongDoanhThuChuyenDi(int[] NguoVeIds, DateTime? TuNgay = null, DateTime? DenNgay = null);
        Decimal GetTongDoanhThuTheoChuyenDi(int[] ChuyenDiIds);
        int GetTongDoanhThuChangTheoChuyenDi(int ChangId, int[] ChuyenDiIds);
       int GetSoKhachLenTaiDiemTheoChuyenDi(int DiemId, int ChuyenId);
       int GetSoKhachXuongTaiDiemTheoChuyenDi(int DiemId, int ChuyenId);
       decimal GetTongDoanhThuChangTheoTuyen(int ChangId, int[] ChuyenDiIds);
        #endregion
       
    }
}
