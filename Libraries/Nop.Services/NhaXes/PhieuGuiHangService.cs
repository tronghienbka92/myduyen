using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public class PhieuGuiHangService : IPhieuGuiHangService
    {
        #region Ctor
        
         private readonly IRepository<PhieuGuiHang> _phieuguihangRepository;
         private readonly IRepository<NhanVien> _nhanvienRepository;
         private readonly IPhoiVeService _phoiveService;
         private readonly INhaXeService _nhaxeService;
        public PhieuGuiHangService(
             IRepository<PhieuGuiHang> phieuguihangRepository,
            IRepository<NhanVien> nhanvienRepository,
            IPhoiVeService phoiveservice,
            INhaXeService nhaxeService
            )
        {
            
            this._phieuguihangRepository=phieuguihangRepository;
            this._phoiveService = phoiveservice;
            this._nhanvienRepository = nhanvienRepository;
            this._nhaxeService = nhaxeService;
        }
        #endregion
        #region phoi ve    
        public virtual PagedList<PhieuGuiHang> GetAllPhieuGuiHang(int NhaXeId = 0,int vanphongguid=0, string _maphieu = "", string _tennguoigui = "",
            ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All, DateTime? ngaytao = null,
            int vanphongnhanid = 0,
               int pageIndex = 0,
               int pageSize = int.MaxValue)
        {
            var query = _phieuguihangRepository.Table.Where(m=>m.TinhTrangVanChuyenId!=(int)ENTinhTrangVanChuyen.Huy);
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (!String.IsNullOrWhiteSpace(_maphieu))
                query = query.Where(m => m.MaPhieu.Contains(_maphieu));
            if (!String.IsNullOrWhiteSpace(_tennguoigui))
                query = query.Where(m => (m.NguoiGui.HoTen.Contains(_tennguoigui) || m.NguoiNhan.HoTen.Contains(_tennguoigui)) );
            if (TinhTrangVanChuyenId > 0)
                query = query.Where(m => m.TinhTrangVanChuyenId == (int)TinhTrangVanChuyenId);
            if(ngaytao.HasValue)
                query = query.Where(c =>c.NgayTao.Year==ngaytao.Value.Year
                    && c.NgayTao.Month==ngaytao.Value.Month
                    && c.NgayTao.Day==ngaytao.Value.Day);
            if(vanphongnhanid>0)
                query = query.Where(m => m.VanPhongNhanId == vanphongnhanid);
            if (vanphongguid > 0)
                query = query.Where(m => m.VanPhongGuiId == vanphongguid);
            query = query.OrderByDescending(m => m.Id);
             return new PagedList<PhieuGuiHang>(query, pageIndex, pageSize);
        }
        public virtual PhieuGuiHang  GetPhieuGuiById(int Id)
        {
            var item= _phieuguihangRepository.GetById(Id);
            //tinh theo thong tin tong tien cuoc va khoi luong
            item.TongTienCuoc = item.HangHoas.Sum(c=>c.GiaCuoc*c.SoLuong);
            item.TongKhoiLuong = item.HangHoas.Sum(c => c.CanNang * c.SoLuong);
            item.TongSoKien = item.HangHoas.Sum(c => c.SoLuong);
            return item;
        }

        public virtual List<PhieuGuiHang> GetAll(int NhaXeId, int XeXuatBenId = 0, int VanPhongNhanId = 0, int VanPhongGuiId = 0, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All)
       {
           var query = _phieuguihangRepository.Table.Where(c=>c.NhaXeId==NhaXeId);
           if (XeXuatBenId > 0)
               query = query.Where(c => c.XeXuatBenId == XeXuatBenId);
           if (VanPhongNhanId > 0)
               query = query.Where(c => c.VanPhongNhanId == VanPhongNhanId);
           if (VanPhongGuiId > 0)
               query = query.Where(c => c.VanPhongGuiId == VanPhongGuiId);
           if (TinhTrangVanChuyenId !=ENTinhTrangVanChuyen.All)
               query = query.Where(c => c.TinhTrangVanChuyenId == (int)TinhTrangVanChuyenId);
           query = query.OrderByDescending(m => m.Id);
           return query.ToList();
       }
      
    
        public virtual List<PhieuGuiHang> GetAllByNhanVien(int NhaXeId,int NhanVienId,DateTime NgayThucHien, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All)
        {
            var query = _phieuguihangRepository.Table.Where(m => m.NhaXeId == NhaXeId && m.NhanVienThuTienId == NhanVienId
                && m.DaThuCuoc
                && m.NgayThanhToan.HasValue
                && m.NgayThanhToan.Value.Day== NgayThucHien.Day
                && m.NgayThanhToan.Value.Month == NgayThucHien.Month
                && m.NgayThanhToan.Value.Year == NgayThucHien.Year
                && m.TinhTrangVanChuyenId!=(int)ENTinhTrangVanChuyen.Huy
                );
            if (TinhTrangVanChuyenId != ENTinhTrangVanChuyen.All)
                query = query.Where(c => c.TinhTrangVanChuyenId == (int)TinhTrangVanChuyenId);
            query = query.OrderByDescending(m => m.Id);
            return query.ToList();
        }
        public virtual List<PhieuGuiHang> GetDetailDoanhThuKiGuiNotPay(int NhaXeId, int NhanVienId, DateTime NgayThucHien, ENTinhTrangVanChuyen TinhTrangVanChuyenId = ENTinhTrangVanChuyen.All)
        {
            var query = _phieuguihangRepository.Table.Where(m => m.NhaXeId == NhaXeId && m.NguoiTaoId == NhanVienId
                && m.NgayTao.Day == NgayThucHien.Day
                && (!m.DaThuCuoc || (m.DaThuCuoc && m.NguoiTaoId != m.NhanVienThuTienId))
                && m.NgayTao.Month == NgayThucHien.Month
                && m.NgayTao.Year == NgayThucHien.Year
                && m.TinhTrangVanChuyenId != (int)ENTinhTrangVanChuyen.Huy
                );
            if (TinhTrangVanChuyenId != ENTinhTrangVanChuyen.All)
                query = query.Where(c => c.TinhTrangVanChuyenId == (int)TinhTrangVanChuyenId);
            query = query.OrderByDescending(m => m.Id);
            return query.ToList();
        }
        /// <summary>
       /// lay danh sach phieu gui theo danh sach id
        /// </summary>
        /// <param name="PhieuGuiHangIds"></param>
        /// <returns></returns>
       public virtual IList<PhieuGuiHang> GetPhieuGuiHangsByIds(int[] PhieuGuiHangIds)
       {
           if (PhieuGuiHangIds == null || PhieuGuiHangIds.Length == 0)
               return new List<PhieuGuiHang>();
           var query = _phieuguihangRepository.Table.Where(c => PhieuGuiHangIds.Contains(c.Id));
           //var query = from c in _phieuguihangRepository.Table
           //            where PhieuGuiHangIds.Contains(c.Id)
           //            select c;
           query = query.OrderByDescending(m => m.Id);
           return query.ToList();
           
       }
      
        public virtual void InsertPhieuGuiHang(PhieuGuiHang item)
        {
            if (item == null)
                throw new ArgumentNullException("Phiếu gửi hàng");
            _phieuguihangRepository.Insert(item);
            //lay tong phieu gui hang trong thang cua nha xe hien tai
            item = GetPhieuGuiById(item.Id);
            //add by lent: 24-12-2015
            var intcount = _phieuguihangRepository.Table.Where(c => c.NhaXeId == item.NhaXeId && c.NgayTao.Month == item.NgayTao.Month && c.NgayTao.Year == item.NgayTao.Year).Count();
            if(item.VanPhongGui==null)
            {
                item.VanPhongGui = _nhaxeService.GetVanPhongById(item.VanPhongGuiId);
            }
            if (item.VanPhongNhan == null)
            {
                item.VanPhongNhan = _nhaxeService.GetVanPhongById(item.VanPhongNhanId);
            }
            item.MaPhieu = string.Format("{2}{0}{1}", DateTime.Now.ToString("yy"), intcount.ToString().PadLeft(4, '0'), item.VanPhongGui.Ma);
            if (String.IsNullOrEmpty(item.DiemGui) && item.VanPhongGui!=null)
                item.DiemGui=item.VanPhongGui.TenVanPhong;
            if (String.IsNullOrEmpty(item.DiemTra) && item.VanPhongNhan != null)
                item.DiemTra = item.VanPhongNhan.TenVanPhong;
            _phieuguihangRepository.Update(item);
        }        
        public virtual void UpdatePhieuGuiHang(PhieuGuiHang item)
        {
            if (item == null)
                throw new ArgumentNullException("Phiếu gửi hàng");
            item.NgayUpdate = DateTime.Now;           
            _phieuguihangRepository.Update(item);

        }
        public virtual void DeletePhieuGuiHang(PhieuGuiHang item)
        {

            item.TinhTrangVanChuyen = ENTinhTrangVanChuyen.Huy;
            item.NgayUpdate = DateTime.Now;
            _phieuguihangRepository.Update(item);
        }

      
        #endregion
        #region Bao cao
        public virtual List<ThongKeItem> GetDoanhThuNhanvien(DateTime tuNgay, DateTime denNgay, int NhaXeid, int VanPhongId)
        {
             var tkitems = _phieuguihangRepository.Table
               .Where(c => c.NhaXeId == NhaXeid 
                   && c.DaThuCuoc     
                   && c.NhanVienThuTienId.HasValue
                   && c.NgayThanhToan.HasValue
                   && c.TinhTrangVanChuyenId != (int)ENTinhTrangVanChuyen.Huy
                   && c.NgayThanhToan >= tuNgay && c.NgayThanhToan <= denNgay)
            .Select(c => new
            {
                nhanvienid = c.NhanVienThuTienId.Value,
                TongDoanhThu = c.HangHoas.Sum(h => h.GiaCuoc * h.SoLuong),
                NgayThu = c.NgayThanhToan.Value
            })
            .GroupBy(c => new {c.NgayThu.Year,c.NgayThu.Month,c.NgayThu.Day , c.nhanvienid})
            .Select(g => new ThongKeItem
            {
                
                ItemDataYear=g.Key.Year,
                ItemDataMonth=g.Key.Month,
                ItemDataDay=g.Key.Day,
                ItemId = g.Key.nhanvienid,
                GiaTri = g.Sum(a => a.TongDoanhThu),
                SoLuong = g.Count()
            })
            //.OrderByDescending(sx => sx.ItemDataDate)
            .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in tkitems)
            {
                var _itemdate = Convert.ToDateTime(item.ItemDataDay + "/" + item.ItemDataMonth + "/" + item.ItemDataYear);
                item.Nhan = _itemdate.ToString("dd-MM-yyyy");
                item.NhanSapXep = _itemdate.ToString("yyyyMMdd");
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == item.ItemId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    tknhanvien.Add(item);
            }
            return tknhanvien;         
        }
        public virtual List<ThongKeItem> GetDoanhThuKiGuiNotPay(DateTime tuNgay, DateTime denNgay, int NhaXeid, int VanPhongId)
        {
            var tkitems = _phieuguihangRepository.Table               
               .Where(c => c.NhaXeId == NhaXeid
                   && c.VanPhongGuiId == VanPhongId
                   && (!c.DaThuCuoc || (c.DaThuCuoc && c.NguoiTaoId!=c.NhanVienThuTienId))
                   && c.TinhTrangVanChuyenId != (int)ENTinhTrangVanChuyen.Huy
                   && c.NgayTao >= tuNgay && c.NgayTao <= denNgay)
                   
            .Select(c => new
            {
                nhanvienid = c.NguoiTaoId,
                TongDoanhThu = c.HangHoas.Sum(h => h.GiaCuoc * h.SoLuong),                
                NgayThu = c.NgayTao,
                
            })
            .GroupBy(c => new {c.NgayThu.Year,c.NgayThu.Month,c.NgayThu.Day, c.nhanvienid })
            .Select(g => new ThongKeItem
            {
                ItemDataYear =g.Key.Year,
                ItemDataMonth=g.Key.Month,
                ItemDataDay=g.Key.Day,
                ItemId = g.Key.nhanvienid,
                GiaTri = g.Sum(a => a.TongDoanhThu),
                SoLuong = g.Count()
            })
            .OrderByDescending(sx => sx.ItemDataYear)
            .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in tkitems)
            {
                var _itemdate=Convert.ToDateTime(item.ItemDataDay + "/" + item.ItemDataMonth + "/" + item.ItemDataYear);
                item.Nhan = _itemdate.ToString("dd-MM-yyyy");
                item.NhanSapXep = _itemdate.ToString("yyyyMMdd");
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == item.ItemId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    tknhanvien.Add(item);
            }
            return tknhanvien;
        }
        #endregion
        #region thong ke theo ki gui hang hoa
        public List<ThongKeItem> GetAllPhieuGuiHangByCuoc(int thang, int nam, int nhaxeid, ENBaoCaoChuKyThoiGian ChuKyThoiGianId)
        {

            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangThang)
                thang = 0;
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNam)
                nam = 0;

            var phieuguis = _phieuguihangRepository.Table
                .Where(c => c.NhaXeId == nhaxeid && c.DaThuCuoc
                && ((c.NgayThanhToan.HasValue && c.NgayThanhToan.Value.Month == thang) || thang == 0)
                && ((c.NgayThanhToan.HasValue && c.NgayThanhToan.Value.Year == nam) || nam == 0))
             .ToList();
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNgay)
            {
                var doanhthungay = phieuguis.GroupBy(c => c.NgayThanhToan.Value.Day).Select(g => new ThongKeItem
                {
                    Nhan = g.Key.ToString(),
                    NhanSapXep = g.Key.ToString(),
                    GiaTri = g.Sum(a => (a.HangHoas.Sum(c => c.GiaCuoc * c.SoLuong))),
                    SoLuong = g.Count()


                }).ToList();

                return doanhthungay;

            }
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangThang)
            {
                var doanhthuthang = phieuguis.GroupBy(c => c.NgayThanhToan.Value.Month).Select(g => new ThongKeItem
                {
                    Nhan = g.Key.ToString(),
                    NhanSapXep = g.Key.ToString(),
                    GiaTri = g.Sum(a => (a.HangHoas.Sum(c => c.GiaCuoc * c.SoLuong))),
                    SoLuong = g.Count()
                }).ToList();
                return doanhthuthang;
            }
            if (ChuKyThoiGianId == ENBaoCaoChuKyThoiGian.HangNam)
            {
                var doanhthunam = phieuguis.GroupBy(c => c.NgayThanhToan.Value.Year).Select(g => new ThongKeItem
                {
                    Nhan = g.Key.ToString(),
                    NhanSapXep = g.Key.ToString(),
                    GiaTri = g.Sum(a => (a.HangHoas.Sum(c => c.GiaCuoc * c.SoLuong))),
                    SoLuong = g.Count()
                }).ToList();
                return doanhthunam;
            }

            return null;

        }
        public virtual decimal HangHoaDoanhThuVanPhong(List<int> nhavienids, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong)
        {
            int Thang1, Thang2;
            _phoiveService.ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var phieuhangs = _phieuguihangRepository.Table.Where(c => c.NhanVienThuTienId.HasValue && nhavienids.Contains(c.NhanVienThuTienId.Value)
                && c.DaThuCuoc 
                && c.NgayThanhToan.Value.Year==nam
                && (thang == 0 || (c.NgayThanhToan.Value.Month >= Thang1 && c.NgayThanhToan.Value.Month <= Thang2))
                )               
                .Select(c => new DoanhThuItem
                {
                    Ngay = c.NgayThanhToan.Value.Day,
                    Thang = c.NgayThanhToan.Value.Month,
                    Nam = c.NgayThanhToan.Value.Year,
                    DoanhThu = c.HangHoas.Sum(g=>g.GiaCuoc * g.SoLuong)
                   
                }).ToList();
            SoLuong = phieuhangs.Count;
            return _phoiveService.GetTongDoanhThu(phieuhangs, thang, nam, QuyId, LoaiThoiGianId);
        }
      
        #endregion
    }
}
