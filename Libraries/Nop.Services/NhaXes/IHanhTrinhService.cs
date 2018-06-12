using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Directory;
using System;

namespace Nop.Services.NhaXes
{
    public partial interface IHanhTrinhService
    {
        #region Diem Don
        PagedList<DiemDon> GetAllDiemDon(int NhaXeId = 0, string _keyword = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue);
        List<DiemDon> GetAllDiemDonByNhaXeId(int NhaXeId);
        DiemDon GetDiemDonById(int itemId);
        void InsertDiemDon(DiemDon _item);
        void UpdateDiemDon(DiemDon _item);
        void DeleteDiemDon(DiemDon _item);
        List<DiemDon> GetAllDiemDonByNhaXeId(int NhaXeId, ENLoaiDiemDon[] loais = null);
        #endregion
        #region HanhTrinh
        PagedList<HanhTrinh> GetAllHanhTrinh(int NhaXeId = 0, string _keyword = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue);
        List<HanhTrinh> GetAllHanhTrinhByNhaXeId(int NhaXeId, int VanPhongId = 0,int TuyenId=0);
        List<HanhTrinh> GetAllHanhTrinhByNhaXeId(int NhaXeId, int[] VanPhongIds);
        HanhTrinh GetHanhTrinhById(int itemId);
        bool InsertHanhTrinh(HanhTrinh _item);
        bool UpdateHanhTrinh(HanhTrinh _item);
        void DeleteHanhTrinh(HanhTrinh _item);

        List<HanhTrinhDiemDon> GetAllHanhTrinhDiemDonByHanhTrinhId(int HanhTrinhId);       
        HanhTrinhDiemDon GetHanhTrinhDiemDonById(int itemId);
        NguonVeXe GetNguonVeXeToanTuyen(int LichTrinhId);
       
        bool InsertHanhTrinhDiemDon(HanhTrinhDiemDon _item);
        bool UpdateHanhTrinhDiemDon(HanhTrinhDiemDon _item);
        void DeleteHanhTrinhDiemDon(HanhTrinhDiemDon _item);
        void DeleteHanhTrinhDiemDon(int HanhTrinhId);
        DiemDon GetDiemDonByHanhTrinhDiemDonId(int itemId);

        List<HanhTrinhDiemChot> GetAllHanhTrinhDiemChotByHanhTrinhId(int HanhTrinhId);
        HanhTrinhDiemChot GetHanhTrinhDiemChotById(int itemId);
        bool InsertHanhTrinhDiemChot(HanhTrinhDiemChot _item);
        bool UpdateHanhTrinhDiemChot(HanhTrinhDiemChot _item);
        void DeleteHanhTrinhDiemChot(HanhTrinhDiemChot _item);
        void DeleteHanhTrinhDiemChot(int HanhTrinhId);
        #endregion
        #region LichTrinh
        PagedList<LichTrinh> GetAllLichTrinh(int NhaXeId = 0, int HanhTrinhId = 0,
         int pageIndex = 0,
         int pageSize = int.MaxValue);
        List<LichTrinh> GetAllLichTrinhByHanhTrinhId(int HanhTrinhId, int NhaXeId = 0);
        List<NguonVeXe> GetAllNguonVeXeByHanhTrinhLoaiXe(List<int> LichTrinhIds, int LoaiXeId);
        LichTrinh GetLichTrinhById(int itemId);
        bool InsertLichTrinh(LichTrinh _item);
        bool UpdateLichTrinh(LichTrinh _item);
        void DeleteLichTrinh(LichTrinh _item);
        List<LichTrinhGiaVe> GetLichTrinhGiaVeByLichTrinhId(int LichTrinhId);
        LichTrinhGiaVe GetLichTrinhGiaVeById(int itemId);
        bool InsertLichTrinhGiaVe(LichTrinhGiaVe _item);
        bool UpdateLichTrinhGiaVe(LichTrinhGiaVe _item);
        void DeleteLichTrinhGiaVe(LichTrinhGiaVe _item);
        void DeleteLichTrinhGiaVe(int LichTrinhId);
        #endregion
        #region Nguon ve xe
        List<NguonVeXe> GetAllNguonVeXeByVeGoc(int NhaXeId, int NguonVeXeGocId);
        List<NguonVeXe> GetAllNguonVeXeByHanhTrinh(List<int> LichTrinhIds);
       // List<NguonVeXe> GetAllNguonVeXe(int NhaXeId = 0, int LichTrinhId = 0, int HanhTrinhId = 0,int DiemDenGocId=0);
        List<NguonVeXe> GetAllNguonVeXe(int NhaXeId = 0, int LichTrinhId = 0, int HanhTrinhId = 0, int DiemDonGocId = 0, int DiemDenGocId = 0, ENKhungGio khunggio = ENKhungGio.All);
        List<NguonVeXe> GetAllNguonVeXeToXuatBen(int NhaXeId = 0, int LichTrinhId = 0, int HanhTrinhId = 0);
        NguonVeXe GetNguonVeXeById(int itemId);
        void InsertNguonVecon(LichTrinh _item, NguonVeXe nguonve);
        void InsertNguonVeGoc(LichTrinh _item);
        void UpdateNguonVeXe(NguonVeXe _item);
        void DeleteNguonVeXe(NguonVeXe _item);
        void DeletePhysicalNguonVe(NguonVeXe item);

        #endregion
        #region province
        StateProvince GetStateProvinceByNguon(int NguonId);
        #endregion
        #region hanh trinh gia ve
        List<HanhTrinhGiaVe> GetallHanhTrinhGiaVe(int HanhTrinhId = 0, int NhaXeId = 0, int DiemDonId = 0, int DiemDenId = 0, int LoaiXeId = 0);

        HanhTrinhGiaVe GetHanhTrinhGiaVeId(int itemId);
        void InsertHanhTrinhGiaVe(HanhTrinhGiaVe _item);
        void UpdateHanhTrinhGiaVe(HanhTrinhGiaVe _item);
        void DeleteHanhTrinhGiaVe(HanhTrinhGiaVe _item);
        #endregion
        #region Tuyen hanh trinh
        List<TuyenHanhTrinh> GetAllTuyenByNhaXeId(int NhaXeId);
        TuyenHanhTrinh GetTuyenById(int itemId);
        void InsertTuyen(TuyenHanhTrinh _item);
        void UpdateTuyen(TuyenHanhTrinh _item);
        void DeleteTuyen(TuyenHanhTrinh _item);
        #endregion
        NguonVeXe GetNguonVeXeByloaixe(int HanhTrinhId, DateTime GioDi, int LoaiXeId);
    }
}
