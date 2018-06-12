
namespace Nop.Core.Domain.NhaXes
{
    public enum ENTrangThaiChuyenPhat
    {
        All = 0,
        Moi = 1,
        DaXepLenh = 2,
        DangVanChuyen = 3,
        DenVanPhongNhan = 4,
        KetThuc = 5,
        Huy = 6


    }
    public enum ENLoaiPhieuChuyenPhat
    {
        All = 0,
        ThuTaiVanPhong = 1,
        ThuTanNoi = 2
    }
    public enum ENNhomPhieuChuyenPhat
    {
        All = 0,
        VP = 10,
        VP_VT = 11,
        VP_CT = 12,
        VP_GT = 13,
        TN = 20,
        TN_VT = 21,
        TN_CT = 22,
        TN_GT = 23,
    }
    public enum ENTrangThaiPhieuVanChuyen
    {
        All = 0,
        Moi = 1,
        DangVanChuyen = 3,
        DenVanPhongNhan = 4,
        KetThuc = 5
    }
    public enum ENLoaiPhieuVanChuyen
    {
        All = 0,
        TrongTuyen = 1,
        VuotTuyen = 2
    }
    public enum ENTrangThaiHangTrongKho
    {

        HangTon = 1,
        HangThatLac = 2


    }
    public enum ENKieuVanPhong
    {
        /// <summary>
        /// Simple product
        /// </summary>
        TruSo = 1,
        /// <summary>
        /// Grouped product
        /// </summary>
        VanPhong = 2,
    }
    public enum ENTinhTrangQuyen
    {
        DANG_SU_DUNG = 1,
        SU_DUNG_HET = 0,

    }
    public enum ENTrangThaiXe
    {
        HoatDong = 1,
        BaoDuong = 2,
        DungHoatDong = 3,
        Huy = 4
    }
    public enum ENKieuXe
    {
        All=0,
        GheNgoi = 1,
        GiuongNam = 2
        
    }
    public enum ENLoaiDiemDon
    {
        BenXuatPhatKetThuc=1,
        GiuaHanhTrinh = 2,
        DiemChot = 3
    }
    
    public enum ENKieuNhanVien
    {
        QuanLy=1,
        LeTan=2,
        KeToan=3,
        LaiXe=4,
        PhuXe=5,        
        Khac=6,
        HanhChinh=7,
        ChotXe = 8
    }
    
    public enum ENGioiTinh
    {
        Nam=1,
        Nu=2
    }
    public enum ENTrangThaiNhanVien
    {
        DangLamViec = 1,
        Nghi = 2
    }
   
    public enum ENBaoCaoQuy
    {
        Quy1=1,
        Quy2=2,
        Quy3=3,
        Quy4=4

    }
    public enum ENLoaiHangHoa
    {
        /// <summary>
        /// hàng xốp, đồ ăn, hàng dễ vỡ
        /// </summary>
        XopDeVo = 1,
        /// <summary>
        /// hàng nặng: xe máy, xe đạp
        /// </summary>
        KimLoaiNang = 2,
        /// <summary>
        /// Linh kiện, thiet bị điện tử: điện thoại, laptop
        /// </summary>
        ThietBiDienTu=3,
        Tien = 4,
        /// <summary>
        /// giấy tờ phong bì, tài liệu
        /// </summary>
        GiayTo = 5,
        LoaiKhac=6

    }
    public enum ENTinhTrangVanChuyen
    {
        All=0,
        ChuaVanChuyen = 1,
        DangVanChuyen = 2,
        NhanHang = 3,
        Huy=4,
        KetThuc=5
        

    }
    public enum ENBaoCaoLoaiThoiGian
    {
        TheoThang = 1,
        TheoQuy = 2,
        TheoNam = 3

    }
    public enum ENBaoCaoChuKyThoiGian
    {
        HangNgay = 1,
        HangThang = 2,
        HangNam = 3
    }
    
    /// <summary>
    /// 0: phoi ve, 1:dung cho chuyen ve, 2: in phoive
    /// </summary>
    public enum ENPhanLoaiPhoiVe
    {
        PHOI_VE=0,
        CHUYEN_VE=1,
        IN_PHOI_VE=2
    }
    public enum ENKieuDuLieu
    {
        SO=1,
        KY_TU=2,
        NGAY_THANG=3,
        PHAN_TRAM=4,
    }
    public enum ENTrangThaiXeXuatBen
    {
        ALL=0,
        CHO_XUAT_BEN=1,
        DANG_DI=2,
        KET_THUC=3,
        HUY=4
    }
    public enum ENNhaXeCauHinh
    {
        //cau hinh chung
        GIAM_GIA_CHO_TRE_EM=1,
        SO_TIEN_LUOT_CHO_LAIXE = 2,
        SO_TIEN_LUOT_CHO_PHUXE = 3,
        TI_LE_TINH_LUONG_LAIPHUXE = 4,
        TIEN_AN_LAI_PHU_XE=5,
        TIEN_CAU_DUONG = 6,
        HEADER_BAO_CAO = 7,
        TIEN_PHOI_LENH=8,
        TIEN_GUI_XE_QUA_DEM=9,

        //ve, phoi ve,
        VE_MAU_IN_PHOI=200,
        VE_MAU_IN_PHOI_PAGES = 2001,
        VE_MAU_IN_PHOI_REPEATSTARTEND = 2002,
        PHIEU_THU_CHI_PHI=203,
        PHIEU_THU_CHI_PHI_LIEN=2003,
        PHIEU_CHI = 204,
        PHIEU_CHI_LIEN = 2004,
        PHIEU_THU = 205,
        PHIEU_THU_LIEN = 2005,

        VE_MAU_IN_CUONG_VE=201,
        VE_MAU_IN_CUONG_VE_LIEN = 2010,

        //ky gui
        //DICH VU GIA TRI GIA TANG
        KY_GUI_DVGT_GIA_TRI=300,
        KY_GUI_DVGT_CONG_KENH=301,
        KY_GUI_DVGT_DIEN_TU_DE_VO = 302,
        KY_GUI_DVGT_NHE_CONG_KENH = 303,
        //xe xuat ben
        KY_GUI_MAU_HANG_HOA_XUAT_BEN=310,
        KY_GUI_MAU_HANG_HOA_XUAT_BEN_PAGES = 3101,
        KY_GUI_MAU_HANG_HOA_XUAT_BEN_REPEATSTARTEND = 3102,
       
        //mau phieu gui hang hoa
        KY_GUI_PHIEU_GUI_HANG=311,
        KY_GUI_PHIEU_GUI_HANG_LIEN = 3110,

        //cau hinh khac
        SMS_TEMPLATE = 400,
        SMS_AUTO_SEND = 401
        

    }
    public enum ENGiaoDichKeVeTrangThai
    {
        ALL = 0,
        MOI_TAO = 1,
        DANG_CHINH_SUA = 2,
        HOAN_THANH = 3,
        HUY = 4
    }
    public enum ENVeXeItemTrangThai
    {
        ALL = 0,
        MOI_TAO = 1,
        DA_GIAO_HANG = 2,
        DA_BAN = 3,
        DA_SU_DUNG = 4,
        HUY = 9
    }
    public enum ENGiaoDichKeVeMenhGiaAction
    {
        MOI = 1,
        SUA = 2,
        SUA_VA_XOA = 3
    }
    public enum ENGiaoDichKeVePhanLoai
    {
        KE_VE = 0,
        TRA_VE = 1
    }
    public enum ENLoaiVeXeItem
    {
        ALL = 0,
        VeQuay = 1,
        VeOnline = 3,
        VeThuong = 5

    }
    public enum ENLoaiTaiChinhThuChi
    {
        //cai nao duoi 100 la thu, tren 100 la chi
        //thu
        PHAT_DAU = 1,
        PHAT_VI_PHAM_LAI_XE = 2,
        PHAT_VI_PHAM_NTV = 3,
        THU_KHAC = 4,

        //chi
        CHI_BEN_XE = 101,
        GUI_XE_QUA_DEM = 102,
        CHI_PHI_BAN_VE = 103,
        RUA_XE = 104,
        CHI_THUONG_DAU = 105,
        CHI_PT_LAI_XE = 106,
        CHI_PT_NTV = 107,
        CHI_CAU_DUONG = 108,
        CHI_KHAC = 109,
        CHI_NHA_HANG = 110,
        CHI_VE_THANG = 111,
        CHI_TIEN_DAU=112,
        CHI_CONG_AN=113,
        CHI_SUA_CHUA_XE=114,
        CHI_DANG_KIEM=115
    }
}
