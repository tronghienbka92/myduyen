using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Directory;
using System;

namespace Nop.Services.NhaXes
{
    public partial interface IGiaoDichKeVeXeService
    {
        GiaoDichKeVe Insert(GiaoDichKeVe item);
        MenhGiaVe GetMenhGiaVeByGia(decimal MenhGia);
        MenhGiaVe GetMenhGiaVeById(int MenhGiaId);
        void InsertMenhGiaVe(MenhGiaVe item);
        void UpdateMenhGiaVe(MenhGiaVe item);
        void DeleteMenhGiaVe(MenhGiaVe item);

        void Update(GiaoDichKeVe item);
        void UpdateVeXeItem(VeXeItem item);
        bool InsertGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item);
        bool UpdateGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item);
        bool DeleteGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item);
        GiaoDichKeVe GetGiaoDichKeVeById(int itemId);
        List<MenhGiaVe> GetAllMenhGia(int NhaXeID);
        bool BanGiaoVe(int NhanVienId, int VanPhongId, int NguoiNhanId);
        List<GiaoDichKeVe> GetAllGiaoDichKeVe(int NhaXeID,int PhanLoaiId, string MaGiaoDich, int NhanVienGiaoId, int NhanVienNhanId, DateTime? dtfrom, DateTime? dtto, ENGiaoDichKeVeTrangThai TrangThai = ENGiaoDichKeVeTrangThai.ALL, int NumTop = 100);
        List<GiaoDichKeVeMenhGia> GetTonGiaoDichKeVeMenhGia(int NhanVienId, int MenhGiaVeId, bool isVeDi,int VanPhongId, int LoaiVeId);
        GiaoDichKeVeMenhGia GetGiaoDichKeVeMenhGiaById(int Id);
        List<GiaoDichKeVeMenhGia> GetAllGiaoDichMenhGiaByGiaoDichKeVeId(int giaodichkeveID);
        List<VeXeItem> GetVeXeItems(int nhaxeID, int NhanVienId = 0, ENVeXeItemTrangThai TrangThaiId = ENVeXeItemTrangThai.ALL, int MenhGiaId = 0, int VanPhongId = 0, int MauVeId = 0, string ThongTin = "", int NumRow = 500);
        List<VeXeItem> GetVeXeItems(int XeXuatBenId);
        void UpdateVeSangDaBan(int[] arrVeId);
        void UpdateVeSangChuaBan(int[] arrVeId);
        void DeleteVeXe(int[] arrVeId);
        List<VeXeItem> GetAllVeXeLuotDi(int nhaxeID);
        List<VeXeItem> GetAllVeXeLuotVe(int nhaxeID);
        List<VeXeItem> GetVeXeBanByDay(int nhaxeID, DateTime NgayBan, int HanhTrinhId = 0, int NhanVienId = 0, ENVeXeItemTrangThai TrangThaiId = ENVeXeItemTrangThai.ALL);
        List<VeXeItem> GetVeXeBanTaiQuay(int NhaXeId, HistoryXeXuatBen chuyenxe,bool isSoHuu=false);
        List<VeXeItem> GetVeXeItemsQuay(int XeXuatBenId);
        List<VeXeItem> GetVeXeBanTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe);
        List<VeXeItem> GetVeXeDuKienBanTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, int SoLuong,List<VeXeItem> arrVeDaCo);
        List<VeXeItem> KiemTraBanVeTaiQuayTheoXe(int NhaXeId, HistoryXeXuatBen chuyenxe, string[] arrseri, out string serikohople);
        List<VeXeItem> KiemTraBanVeTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, string[] arrseri, out string serikohople);
        List<VeXeItem> CapNhatBanVeTaiQuayTheoXe(int NhaXeId, HistoryXeXuatBen chuyenxe, string[] arrseri);
        List<VeXeItem> CapNhatBanVeTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, string[] arrseri);
        VeXeItem GetVeXeItem(int NhaXeId, string SoSeri, string MauVe, string KyHieu);
        VeXeItem GetVeXeItemById(int Id);
        bool isExistVeXeItem(int NhaXeId, string SoSeri, string MauVe, string KyHieu);
        bool isExistVeXeItem(int NhaXeId, string MauVe, string KyHieu, string SoSeriFrom, int SoLuong,bool isKeVe=true);
        bool InsertVeXeItem(VeXeItem itemVeXe);


        bool InsertVeXeItem(IEnumerable<VeXeItem> vexes); //insert toan bo ve cac thuoc tinh cua ve xe, khong can liet ke

        bool InsertGiaoDichKeVeXeItem(GiaoDichKeVeXeItem item);
        bool InsertGiaoDichKeVeXeItem(IEnumerable<GiaoDichKeVeXeItem> items);
        void EmptyGiaoDichKeVeXeItem(int Id);
        int SoVeConLaiCuaQuay(int nhaxeID, int VanPhongId = 0);
        decimal DoanhThuQuayHienTai(int nhaxeID, int VanPhongId = 0);
        void FinishGiaoDichKeVe(int Id);
        void TraVeTheoMenhGia(int Id);
        void ChuyenVeTheoMenhGia(int Id, int NhanVienNhanId);

        bool InsertQuanLyMauVeKyHieu(QuanLyMauVeKyHieu item);
        bool UpdateQuanLyMauVeKyHieu(QuanLyMauVeKyHieu item);
        QuanLyMauVeKyHieu GetMauVeById(int Id);
        List<QuanLyMauVeKyHieu> GetAllMauVeKyHieu(int NhaXeId);
        VeXeItem BanVe(int NhaXeId, int NhanVienId, int XeXuatBenId, int NguonVeXeId, int ChangId, Decimal giave, bool isVeDi);
        void HuyBanVe(int NhaXeId, int NhanVienId, int VeXeItemId);
        List<int> GetMenhGiaId(int NhaxeId);
        VeXeItem SuDungVe(int NhaXeId, int NguonVeXeId, string SoSeri, bool isVeDi);
        VeXeItem BanVeTaiQuay(int NhaXeId, int NhanVienId, int ChangId, Decimal giave, bool isVeDi, DateTime NgayDi, int VanPhongId, int QuyenId = 0);
        bool TieuVe(int NhaXeId, int SeriFrom, int MenhGiaId, int NhanVienId, bool IsVeDi);
        VeXeItem GetVeChuanBiBan(int NhaXeId, int VanPhongId, Decimal giave, bool isVeDi = true);
        VeXeItem GetVeChuanBiBanTaiQuay(int NhaXeId, int VanPhongId, Decimal giave, bool isVeDi);
        List<VeXeItem> GetVeXeBanQuayByDay(int nhaxeID, DateTime NgayBan, int ChangId, int HanhTrinhId = 0, int NhanVienId = 0, Decimal giave = 0, bool isVeDi = true);
        List<VeXeItem> GetAllMenhGia(int nguonveId, int NhaXeId);
        bool KiemTraSeriQuay(int NhaXeId, string[] arrseri);
        bool KiemTranTonTaiSeri(int NhaXeId, List<string> arrSeritext);
        int CountVeConLaiTaiQuay(int nhaxeID, int VanPhongId = 0, decimal MenhGia = 0, bool IsVeDi = true);
        #region giay di duong
        int GetSLVeChangDiDuong(DateTime ngaydi, int NguonVeId, int ChangId, int NhaXeId);
        void DeleteXeXuatBenVeXeItem(int ChuyenDiId, int ChuyenVeId);
        void InsertXeXuatBenVeXeItem(int XeXuatBenId,int ChangId, int[] vexeitems);
        void FinishXuatBenVeXeItem(int ChuyenDiId, int ChuyenVeId);
        void PhoiVeBoSungThemMoi(int ChuyenDiId, int NhanVienId, int ChangId, int MauVeId, int SeriFrom, int SeriTo, string SeriGiamGia);

        void PhoiVeBoSungHuy(int ChuyenDiId, int NhanVienId, int ChangId, int MauVeId, int SeriFrom, int SeriTo);
        List<VeXeItem> GetTonVeXeItemsByNhanVien(int NhanVienId, decimal MenhGia, int MauVeId, int SoLuong);
        #endregion
        #region doanh thu ban ve
        decimal GetTonglDoanhThuXeTheoNgay(DateTime ngaydi, int XeId, int NguonVeId, out int SoLuong,int MenhGiaId=0);
        List<ThongKeItem> VTGetDoanhThuBanVeTheoNgay(DateTime tuNgay, DateTime denNgay, int nhaxeid, int VanPhongId);
        List<ThongKeItem> VTGetDoanhThuBanVeTheoNhanVien(int nhaxeid, int VanPhongId, DateTime NgayBan);
        List<VeXeItem> VTGetDetailDoanhThu(int nhaxeid, DateTime ngaydi, int nhanvienid = 0);
        #endregion
        #region doanh thu nhan vien dinh ki
        decimal VTDoanhThuTungNhanvien(int NhanVienId, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong, int MenhGiaId=0);
        #endregion

        #region Ve xe theo Quyen
        List<VeXeQuyen> GetTonVeXeQuyen(int NhaXeId, int MenhGiaId, int VanPhongId = 0, int NhanVienId = 0);
        void UpdateQuyenVeThuTuBan(int Id, int ThuThuBan);
        #endregion

        #region Thiet Dat so serial ve
        bool GanSoSerial(int PhoiVeId, int NhanVienId, int QuayBanVeId, int MauVeKyHieuId, string MaVe);
        #endregion
        #region Chuyen di tai chinh: thu chi , doanh thu, hoa hong
        void InsertChuyenDiTaiChinh(ChuyenDiTaiChinh item);
        void UpdateChuyenDiTaiChinh(ChuyenDiTaiChinh item);
        void DeleteChuyenDiTaiChinh(ChuyenDiTaiChinh item);
        ChuyenDiTaiChinh GetChuyenDiTaiChinhById(int Id);
        ChuyenDiTaiChinh GetChuyenDiTaiChinhByLuotId(int LuotId);
        void InsertChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item);
        void UpdateChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item);
        void DeleteChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item);
        void DeleteAllChuyenDiTaiChinhThuChi(List<ChuyenDiTaiChinhThuChi> item);
        ChuyenDiTaiChinhThuChi GetChuyenDiTaiChinhThuChiById(int ChuyenDiTaiChinhId, int LoaiThuChiId);
        #endregion
        List<ThongKeVeTiepVienDaDung> GetVeTiepVienTheoQuyen(int nhaxeID, bool IsCon, int Thang, int Nam, string ThongTin);
    }
}



