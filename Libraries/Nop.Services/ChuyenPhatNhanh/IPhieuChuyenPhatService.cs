using Nop.Core;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.ChuyenPhatNhanh
{
    public partial interface IPhieuChuyenPhatService
    {
        #region phieu chuyen phat
        PagedList<PhieuChuyenPhat> GetAllPhieuChuyenPhat(int NhaXeId = 0, int vanphongguid = 0, string _maphieu = "", string _tennguoigui = "",
          ENTrangThaiChuyenPhat TrangThaiId = ENTrangThaiChuyenPhat.All, DateTime? NgayNhanHang = null,
          int vanphongnhanid = 0,
              int pageIndex = 0,
              int pageSize = int.MaxValue);
        List<PhieuChuyenPhat> GetAllPhieuChuyenPhat(int NhaXeId, int vanphonggui_id, DateTime? NgayNhanHang = null, string _thongtin = "", ENTrangThaiChuyenPhat TrangThaiId = ENTrangThaiChuyenPhat.All, int PhieuVanChuyenId = 0, int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        PhieuChuyenPhat GetPhieuChuyenPhatById(int Id);
        IList<PhieuChuyenPhat> GetPhieuChuyenPhatsByIds(int[] PhieuGuiHangIds);
        List<PhieuChuyenPhat> GetAllPhieuChuyenPhatTrongThang(int NhaXeId, int vanphonggui_id=0, DateTime? NgayNhanHang = null, string _thongtin = "", int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null, int Thang = 0, int Nam = 0,DateTime? NgayKetThuc=null,bool isGDNhan=true,int TuyenId=0);
        List<PhieuChuyenPhat> GetPhieuTonVaThatLac(int NhaXeId, string _thongtin = "", int VanPhongNhanId = 0, int TrangThaiId = 0);
        void InsertPhieuChuyenPhat(PhieuChuyenPhat _item);
        void UpdatePhieuChuyenPhat(PhieuChuyenPhat _item);
        void DeletePhieuChuyenPhat(PhieuChuyenPhat _item);
        void InsertPhieuChuyenPhatLog(string GhiChu, int PhieuChuyenPhatId);
        #endregion
        #region phieu chuyen phat nhat ky van chuyen
        PhieuChuyenPhatVanChuyen GetPhieuChuyenPhatVanChuyenById(int PhieuChuyenPhatId,int ChuyenDiId);
        PhieuChuyenPhatVanChuyen GetPhieuChuyenPhatVanChuyen(int PhieuChuyenPhatId, int PhieuVanChuyenId);
        void UpdatePhieuChuyenPhatVanChuyen(PhieuChuyenPhatVanChuyen _item);
        void DeletePhieuChuyenPhatVanChuyen(int Id);
        List<PhieuChuyenPhatVanChuyen> GetAllPhieuChuyenPhatVanChuyen(DateTime NgayGuiHangTu, DateTime NgayGuiHangDen, int VanPhongGuiId = 0, int VanPhongNhanId = 0, string BienSoXe = "", string SoLenh = "", int TuyenId = 0);
        #endregion

        #region khach hang
        PagedList<KhachHang> GetAllKhachHang(int NhaXeId = 0,
              int pageIndex = 0,
              int pageSize = int.MaxValue);
        List<KhachHang> GetAllKhachHang(int NhaXeId, string KeySearch);
        KhachHang GetKhachHangById(int Id);

        void InsertKhachHang(KhachHang _item);
        void UpdateKhachHang(KhachHang _item);
        void DeleteKhachHang(KhachHang _item);
        #endregion
        #region phieu van chuyen
        List<PhieuVanChuyen> GetAllPhieuVanChuyen(int NhaXeId, int VanPhongGuiId = 0, string SoLenh = "", ENTrangThaiPhieuVanChuyen TrangThaiId = ENTrangThaiPhieuVanChuyen.All, DateTime? ngaytao = null, int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        PhieuVanChuyen GetPhieuVanChuyenById(int Id);
        List<PhieuVanChuyen> GetAllPhieuVanChuyenByChuyenId(int ChuyenId,int NhaXeId);
        void InsertPhieuVanChuyen(PhieuVanChuyen _item);
        void UpdatePhieuVanChuyen(PhieuVanChuyen _item);
        void DeletePhieuVanChuyen(PhieuVanChuyen _item);
        int GetSoLenhVanChuyenTiepTheo(int NhaXeId, int VanPhongId);
        #endregion
        #region phieu van chuyen nhat ky van chuyen
        PhieuVanChuyenLog GetPhieuVanChuyenLog(int PhieuVanChuyenId, int ChuyenDiId);
        PhieuVanChuyenLog GetPhieuVanChuyenLogById(int Id);
        void UpdatePhieuVanChuyenLog(PhieuVanChuyenLog _item);
        void DeletePhieuVanChuyenLog(int Id);
        List<PhieuVanChuyenLog> GetAllPhieuVanChuyenLog();
        #endregion
        #region khu vuc
        List<KhuVuc> GetAllKhuVuc(int NhaXeId = 0);
        KhuVuc GetKhuVucById(int Id);
        void InsertKhuVuc(KhuVuc _item);
        void UpdateKhuVuc(KhuVuc _item);
        void DeleteKhuVuc(KhuVuc _item);
        
        #endregion
        #region to van chuyen, to van chuyen van phong
        ToVanChuyenVanPhong GetToVanChuyenVanPhongById(int Id);
        ToVanChuyenVanPhong GetToVanChuyenVanPhong(int VanPhongId, int ToVanChuyenId);
        void InsertToVanChuyenVanPhong(ToVanChuyenVanPhong _item);
        void UpdateToVanChuyenVanPhong(ToVanChuyenVanPhong _item);
        void DeleteToVanChuyenVanPhong(ToVanChuyenVanPhong _item);
        List<VanPhong> GetAllVanPhongByToVanChuyen(int ToVanChuyenId);
        List<ToVanChuyen> GetAllToVanChuyen(int NhaXeId);
      
        ToVanChuyen GetToVanChuyenById(int Id);
        void InsertToVanChuyen(ToVanChuyen _item);
        void UpdateToVanChuyen(ToVanChuyen _item);
        void DeleteToVanChuyen(ToVanChuyen _item);
        List<NguoiVanChuyen> GetAllNguoiVanChuyen(int ToVanChuyenId);
        NguoiVanChuyen GetNguoiVanChuyenById(int Id);
        void InsertNguoiVanChuyen(NguoiVanChuyen _item);
        void UpdateNguoiVanChuyen(NguoiVanChuyen _item);
        void DeleteNguoiVanChuyen(NguoiVanChuyen _item);
        #endregion
       

        
        #region chuyen di & van phong vuot tuyen
        VanPhongVuotTuyen GetVanPhongVuotTuyenByVanPhongNhan(int VanPhongGuiId, int VanPhongCuoiId);
        List<VanPhongVuotTuyen> GetVanPhongVuotTuyenByVanPhong(int VanPhongGuiId = 0, int VanPhongCuoiId = 0);
        List<HistoryXeXuatBen> GetAllChuyenDi(int NhaXeId, int VanPhongGuiId, int VanPhongNhanId, DateTime NgayDi);
        List<VanPhong> GetAllVanPhongByVanPhongId(int NhaXeId, int VanPhongId);

        #endregion
    }
}
