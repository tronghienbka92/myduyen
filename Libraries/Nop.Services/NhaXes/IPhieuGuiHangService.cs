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
    public partial interface IPhieuGuiHangService
    {
        PagedList<PhieuGuiHang> GetAllPhieuGuiHang(int NhaXeId = 0,int vanphongguid=0, string _maphieu = "",string _tennguoigui = "",
            ENTinhTrangVanChuyen TinhTrangVanChuyenId=ENTinhTrangVanChuyen.All,DateTime? ngaytao= null,
            int vanphongnhanid=0,
                int pageIndex = 0,
                int pageSize = int.MaxValue);
        PhieuGuiHang GetPhieuGuiById(int Id);
      
        void InsertPhieuGuiHang(PhieuGuiHang item);
        List<PhieuGuiHang> GetAll(int NhaXeId, int XeXuatBenId = 0, int VanPhongNhanId = 0, int VanPhongGuiId = 0, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All);
        decimal HangHoaDoanhThuVanPhong(List<int> nhavienids, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong);
        List<ThongKeItem> GetAllPhieuGuiHangByCuoc(int thang, int nam, int nhaxeid, ENBaoCaoChuKyThoiGian ChuKyThoiGianId);
        List<PhieuGuiHang> GetAllByNhanVien(int NhaXeId, int NhanVienId, DateTime NgayThucHien, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All);
        IList<PhieuGuiHang> GetPhieuGuiHangsByIds(int[] PhieuGuiHangIds);
        List<ThongKeItem> GetDoanhThuKiGuiNotPay(DateTime tuNgay, DateTime denNgay, int NhaXeid, int VanPhongId);
        void UpdatePhieuGuiHang(PhieuGuiHang item);
        void DeletePhieuGuiHang(PhieuGuiHang item);
        List<PhieuGuiHang> GetDetailDoanhThuKiGuiNotPay(int NhaXeId, int NhanVienId, DateTime NgayThucHien, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All);
        List<ThongKeItem> GetDoanhThuNhanvien(DateTime tuNgay, DateTime denNgay, int NhaXeid, int VanPhongId);

    }
}
