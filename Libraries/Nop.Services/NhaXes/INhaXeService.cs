using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Chonves;
using System;


namespace Nop.Services.NhaXes
{
    public partial interface INhaXeService
    {
        #region Nha Xe Info
        IPagedList<NhaXe> GetAllNhaXe(string nhaxeName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false, 
            int OwnerID=0);
        NhaXe GetNhaXeById(int NhaXeId);
        List<NhaXe> GetAllNhaXe();
        NhaXe GetNhaXeByCustommerId(int CustommerId);
        void InsertNhaXe(NhaXe _item);
        void UpdateNhaXe(NhaXe _item);
        void DeleteNhaXe(NhaXe _item);
        #endregion
        #region NhaXe Picture
        void InsertNhaXePicture(NhaXePicture _item);
        void UpdateNhaXePicture(NhaXePicture _item);
        void DeleteNhaXePicture(NhaXePicture _item);
        IList<NhaXePicture> GetNhaXePicturesByNhaXeId(int NhaXeId);
        NhaXePicture GetNhaXePictureById(int nhaxePictureId);
        #endregion
        #region Van Phong
        IPagedList<VanPhong> GetAllVanPhong(int NhaXeId=0, string tenvanphong = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);
        List<VanPhong> GetAllVanPhongByNhaXeId(int NhaXeId = 0);
        void InsertVanPhong(VanPhong _item);
        void UpdateVanPhong(VanPhong _item);
        void DeleteVanPhong(VanPhong _item);
        VanPhong GetVanPhongById(int VanPhongId);
        #endregion
        #region xe xuat ben

        void DeleteHistoryXeXuatBenNhanVien(int XeXuatBenId);
        List<XeVanChuyen> GetAllBienSoXeByNhaXeId(int NhaXeId,int LoaiXeId=0);
        void InsertHistoryXeXuatBen(HistoryXeXuatBen _item);
        void UpdateHistoryXeXuatBen(HistoryXeXuatBen _item);
        HistoryXeXuatBen GetHistoryXeXuatBenId(int HistoryXeXuatBenId);
        List<HistoryXeXuatBen> GetAllChuyenDiTrongNgay(int NhaXeId, DateTime NgayDi, int HanhTrinhId = 0, ENKhungGio khunggio = ENKhungGio.All, string ThongTin = null, bool isSoNguoi = true, int LoaiXeId = 0, bool istop = false);
        HistoryXeXuatBen GetChuyenVeByChuyenDi(int ChuyenDiId);
        HistoryXeXuatBen GetChuyenDiByChuyenVe(int ChuyenDiId);

        HistoryXeXuatBen GetXeXuatBenByNguonVeId(int nguonvexeid);
        /// <summary>
        /// lay toàn bộ xe xuất bến có ngày đi >= ngày hiện tại trong nha xe
        /// </summary>
        /// <param name="nhaxeid"></param>
        /// <returns></returns>
        List<HistoryXeXuatBen> GetAllXeXuatBenByNgayDi(int NhaXeId,DateTime NgayDi);
        List<NhanVien> GetAllNhanVienByNhaXe(int NhaXeId, ENKieuNhanVien[] kieunvs, string TenNhanVien = "");
        /// <summary>
        /// lay theo nguon ve va ngay di
        /// </summary>
        /// <param name="NguonVeId"></param>
        /// <param name="ngaydi"></param>
        /// <returns></returns>
        List<HistoryXeXuatBen> GetHistoryXeXuatBenByNhaXeId(int NhaXeId);
        
        HistoryXeXuatBen GetHistoryXeXuatBenByNguonVeId(int NguonVeId, DateTime ngaydi);
        List<HistoryXeXuatBen> GetHistoryXeXuatBenByXeVanChuyen(int XeVanChuyenId);
        void InsertHistoryXeXuatBenLog(HistoryXeXuatBenLog _item);
        void UpdateHistoryXeXuatBenLog(HistoryXeXuatBenLog _item);
        void DeleteHistoryXeXuatBenLog(HistoryXeXuatBenLog _item);
        HistoryXeXuatBenLog GetHistoryXeXuatBenLogById(int Id);
        void UpdateHistoryXeXuatBenTrangThai(int Id,int NguoiTaoId, ENTrangThaiXeXuatBen trangthai);
        List<HistoryXeXuatBen> GetHistoryXeXuatBensByNguonVeId(int NguonVeId, DateTime ngaydi);
        List<ThongKeLuotXuatBenItem> GetThongKeLuotXuatBen(int ThangId, int NamId, int[] arrNhanVien);
        #endregion
        #region Nha xe cau hinh
        List<NhaXeCauHinh> GetAllNhaXeCauHinh(int NhaXeId);

        void Insert(NhaXeCauHinh _item);
        void Update(NhaXeCauHinh _item);
        void Delete(NhaXeCauHinh _item);
        NhaXeCauHinh GetNhaXeCauHinhById(int id);
        NhaXeCauHinh GetNhaXeCauHinhByCode(int NhaXeId, ENNhaXeCauHinh cauhinh);
        NhaXeCauHinh GetNhaXeCauHinhByCode(int NhaXeId, string cauhinh);
        Decimal GetGiaTriCauHinh(int NhaXeId, ENNhaXeCauHinh cauhinh);
        #endregion
        #region Chot khach
        void InsertChotKhach(ChotKhach _item);
        void UpdateChotKhach(ChotKhach _item);
        void DeleteChotKhach(ChotKhach _item);
        ChotKhach GetChotKhachById(int Id);
        List<ChotKhach> GetChotKhachs(int NhaXeId, string Ma = null, int HistoryXeXuatBenId = 0, int NguoiChotId = 0, int DiemChotId = 0, DateTime? NgayChotTu = null, DateTime? NgayChotDen = null, int NumTop = 100);

        #endregion
        
    }
}
