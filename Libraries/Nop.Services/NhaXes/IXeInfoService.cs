using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;


namespace Nop.Services.NhaXes
{
    public partial interface IXeInfoService
    {


        #region GheItem
        List<GheItem> GetAllGheItem(int LoaiXeId);
        GheItem GetGheItemById(int itemId);
        void InsertGheItem(GheItem _item);
        void UpdateGheItem(GheItem _item);
        void DeleteGheItem(GheItem _item);
        //quy tac
        List<SoDoGheXeQuyTac> GetAllSoDoGheXeQuyTac(int LoaiXeId);
        SoDoGheXeQuyTac GetSoDoGheXeQuyTacById(int itemId);
        void InsertSoDoGheXeQuyTac(SoDoGheXeQuyTac _item);
        void UpdateSoDoGheXeQuyTac(SoDoGheXeQuyTac _item);
        void DeleteSoDoGheXeQuyTac(SoDoGheXeQuyTac _item);
        void DeleteGheAndSoDoGheXeQuyTac(int LoaiXeId);
        #endregion

        #region xe info


        PagedList<XeVanChuyen> GetAllXeInfo(int NhaXeId = 0, string tenxe = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue);
        List<XeVanChuyen> GetAllXeInfoByNhaXeId(int NhaXeId);
        List<XeVanChuyen> GetAllXeInfoByLoaiXeId(int LoaiXeId);
        XeVanChuyen GetXeInfoById(int itemId);
        XeVanChuyen GetXeInfoByBienSo(int NhaXeId, string BienSoXe);
        void InsertXeInfo(XeVanChuyen _item);
        void UpdateXeInfo(XeVanChuyen _item);
        void DeleteXeInfo(XeVanChuyen _item);
        #endregion

        #region so do ghe xe
        List<SoDoGheXe> GetAllSoDoGheXe(int KieuXeId);
        List<SoDoGheXeViTri> GetAllSoDoGheViTri(int SoDoGheXeId);
        SoDoGheXeViTri GetSoDoGheXeViTri(int SoDoGheXeId, int x, int y);
        SoDoGheXe GetSoDoGheXeById(int itemId);

        PagedList<LoaiXe> GetAll(int NhaXeId = 0, string tenloaixe = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue);
        List<LoaiXe> GetAllByNhaXeId(int NhaXeId);
        LoaiXe GetById(int itemId);
        void Insert(LoaiXe _item);
        void Update(LoaiXe _item);
        void Delete(LoaiXe _item);
        #endregion
        #region Dinh vi xe
       HistoryXeXuatBen DinhVi_GetHistoryXeXuatBenByXeVanChuyen(int XeVanChuyenId);
        #endregion
       List<XeVanChuyen> GetAllXeVanChuyenByNhaXeId(int NhaXeID);
       List<HistoryXeXuatBen> GetAllByXeVanChuyenID(int XeVanChuyenID);
    }
}
