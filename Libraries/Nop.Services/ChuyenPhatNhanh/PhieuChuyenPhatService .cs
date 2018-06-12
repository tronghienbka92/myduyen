using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Shipping;
using Nop.Services.Configuration;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.ChuyenPhatNhanh;
using Nop.Services.NhaXes;

namespace Nop.Services.ChuyenPhatNhanh
{
    public class PhieuChuyenPhatService  : IPhieuChuyenPhatService 
    {
        #region Ctor
        private readonly IRepository<PhieuChuyenPhat> _phieuchuyenphatRepository;
        private readonly IRepository<PhieuChuyenPhatVanChuyen> _phieuchuyenphatvanchuyenRepository;
        private readonly IRepository<PhieuChuyenPhatLog> _phieuchuyenphatlogRepository;
        private readonly IRepository<PhieuVanChuyen> _phieuvanchuyenRepository;
        private readonly IRepository<PhieuVanChuyenLog> _phieuvanchuyenlogRepository;
        private readonly IRepository<KhachHang> _khachhangRepository;
        private readonly IRepository<KhuVuc> _khuvucRepository;
        private readonly IRepository<ToVanChuyen> _tovanchuyenRepository;
        private readonly IRepository<VanPhongVuotTuyen> _vanphongvuottuyenRepository;
        private readonly IRepository<HistoryXeXuatBen> _chuyendiRepository;
        private readonly IRepository<DiemDon> _diemdonRepository;
        private readonly IRepository<HanhTrinh> _hanhtrinhRepository;
   
        private readonly IRepository<NguoiVanChuyen> _nguoivanchuyenRepository;
        private readonly IRepository<ToVanChuyenVanPhong> _tovanchuyenvanphongRepository;
         public PhieuChuyenPhatService(
             IRepository<NguoiVanChuyen> nguoivanchuyenRepository,
             IRepository<HanhTrinh> hanhtrinhRepository,
             IRepository<DiemDon> diemdonRepository,
             IRepository<HistoryXeXuatBen> chuyendiRepository,
             IRepository<PhieuChuyenPhatVanChuyen> phieuchuyenphatvanchuyenRepository,
            IRepository<PhieuChuyenPhat> phieuchuyenphatRepository,
             IRepository<PhieuChuyenPhatLog> phieuchuyenphatlogRepository,
             IRepository<PhieuVanChuyen> phieuvanchuyenRepository,
              IRepository<PhieuVanChuyenLog> phieuvanchuyenlogRepository,
             IRepository<KhachHang> khachhangRepository,
              IRepository<KhuVuc> khuvucRepository,
             IRepository<ToVanChuyen> tovanchuyenRepository,
             IRepository<VanPhongVuotTuyen> vanphongvuottuyenRepository,
         
               IRepository<ToVanChuyenVanPhong> tovanchuyenvanphongRepository
            )
        {
            this._nguoivanchuyenRepository = nguoivanchuyenRepository;
            this._hanhtrinhRepository = hanhtrinhRepository;
            this._diemdonRepository = diemdonRepository;
            this._chuyendiRepository = chuyendiRepository;
            this._phieuchuyenphatvanchuyenRepository = phieuchuyenphatvanchuyenRepository;
            this._phieuchuyenphatRepository = phieuchuyenphatRepository;
            this._phieuchuyenphatlogRepository = phieuchuyenphatlogRepository;
            this._phieuvanchuyenRepository = phieuvanchuyenRepository;
            this._phieuvanchuyenlogRepository = phieuvanchuyenlogRepository;
            this._khachhangRepository = khachhangRepository;
            this._khuvucRepository = khuvucRepository;
            this._tovanchuyenRepository = tovanchuyenRepository;
            this._vanphongvuottuyenRepository = vanphongvuottuyenRepository;
           
            this._tovanchuyenvanphongRepository = tovanchuyenvanphongRepository;
        }
        #endregion
        #region phieu chuyen phat
         public virtual PagedList<PhieuChuyenPhat> GetAllPhieuChuyenPhat(int NhaXeId = 0, int vanphongguid = 0, string _maphieu = "", string _tennguoigui = "",
            ENTrangThaiChuyenPhat TrangThaiId = ENTrangThaiChuyenPhat.All, DateTime? NgayNhanHang = null,
            int vanphongnhanid = 0,
                int pageIndex = 0,
                int pageSize = int.MaxValue)
        {
            var query = _phieuchuyenphatRepository.Table.Where(m => m.TrangThaiId != (int)ENTrangThaiChuyenPhat.Huy);
            query = query.Where(m => m.NhaXeId == NhaXeId);
            if (!String.IsNullOrWhiteSpace(_maphieu))
                query = query.Where(m => m.MaPhieu.Contains(_maphieu));
            if (!String.IsNullOrWhiteSpace(_tennguoigui))
                query = query.Where(m => (m.NguoiGui.HoTen.Contains(_tennguoigui) || m.NguoiNhan.HoTen.Contains(_tennguoigui)));
            if (TrangThaiId > 0)
                query = query.Where(m => m.TrangThaiId == (int)TrangThaiId);
            if (NgayNhanHang.HasValue)
            {
                var _ngaynhanhang = NgayNhanHang.Value.Date;
                query = query.Where(c => c.NgayNhanHang == _ngaynhanhang);
            }
                
            if (vanphongnhanid > 0)
                query = query.Where(m => m.VanPhongNhanId == vanphongnhanid);
            if (vanphongguid > 0)
                query = query.Where(m => m.VanPhongGuiId == vanphongguid);
            query = query.OrderByDescending(m => m.Id);
            return new PagedList<PhieuChuyenPhat>(query, pageIndex, pageSize);

        }
         public virtual List<PhieuChuyenPhat> GetAllPhieuChuyenPhat(int NhaXeId, int vanphonggui_id, DateTime? NgayNhanHang = null, string _thongtin = "", ENTrangThaiChuyenPhat TrangThaiId = ENTrangThaiChuyenPhat.All, int PhieuVanChuyenId = 0, int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
         {
             var query = _phieuchuyenphatRepository.Table.Where(m => m.TrangThaiId != (int)ENTrangThaiChuyenPhat.Huy);
             query = query.Where(m => m.NhaXeId == NhaXeId);
             if (NgayNhanHang.HasValue)
             {
                 var _ngaynhanhang = NgayNhanHang.Value.Date;
                 query = query.Where(c => c.NgayNhanHang == _ngaynhanhang);
             }
            if(TuNgay.HasValue)
            {
                var _tungay=TuNgay.Value.Date;
                query = query.Where(c => c.NgayNhanHang > _tungay);
            }
            if (DenNgay.HasValue)
            {
                var _dengay = DenNgay.Value.Date.AddDays(1);
                query = query.Where(c => c.NgayNhanHang < _dengay);
            }
             if (TrangThaiId !=ENTrangThaiChuyenPhat.All)
                 query = query.Where(m => m.TrangThaiId == (int)TrangThaiId);
             if (vanphonggui_id > 0)
                 query = query.Where(m => m.VanPhongGuiId == vanphonggui_id);

             if (!String.IsNullOrWhiteSpace(_thongtin))
             {
                 query = query.Where(m => (m.MaPhieu.Contains(_thongtin) || m.NguoiGui.HoTen.Contains(_thongtin) || m.NguoiGui.SoDienThoai.Contains(_thongtin) || m.NguoiNhan.SoDienThoai.Contains(_thongtin) || m.NguoiNhan.HoTen.Contains(_thongtin) || m.TenHang.Contains(_thongtin)));
             }
             if(PhieuVanChuyenId>0)
             {
                 query = query.Where(m => m.PhieuVanChuyenId == PhieuVanChuyenId);
             }
             if (VanPhongNhanId > 0)
             {
                
                 query = query.Where(m => m.phieuvanchuyen.nhatkyvanchuyens.Any(nk => nk.VanPhongNhanId == VanPhongNhanId)
                     || (
                        m.VanPhongNhanId == VanPhongNhanId
                        && m.phieuvanchuyen.LoaiPhieuVanChuyenId == (int)ENLoaiPhieuVanChuyen.TrongTuyen
                        )                     
                     );
             }
             query = query.OrderByDescending(m => m.Id);
             return query.ToList();
         }
         public virtual IList<PhieuChuyenPhat> GetPhieuChuyenPhatsByIds(int[] PhieuGuiHangIds)
         {
             if (PhieuGuiHangIds == null || PhieuGuiHangIds.Length == 0)
                 return new List<PhieuChuyenPhat>();
             var query = _phieuchuyenphatRepository.Table.Where(c => PhieuGuiHangIds.Contains(c.Id));           
             return query.ToList();

         }
         public virtual List<PhieuChuyenPhat> GetAllPhieuChuyenPhatTrongThang(int NhaXeId, int vanphonggui_id=0, DateTime? NgayNhanHang = null, string _thongtin = "",int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null,int Thang=0,int Nam=0, DateTime? NgayKetThuc=null,bool isGDNhan=true, int TuyenId=0)
         {
             var arr=new int[] {(int)ENTrangThaiChuyenPhat.DangVanChuyen,(int)ENTrangThaiChuyenPhat.DenVanPhongNhan,(int)ENTrangThaiChuyenPhat.KetThuc};

             var query = _phieuchuyenphatRepository.Table.Where(m => arr.Contains(m.TrangThaiId));
             query = query.Where(m => m.NhaXeId == NhaXeId);
             if (NgayNhanHang.HasValue)
             {
                 var _ngaynhanhang = NgayNhanHang.Value.Date;
                 query = query.Where(c => c.NgayNhanHang == _ngaynhanhang);
             }
             if (NgayKetThuc.HasValue)
             {
                 var _ngayketthuc = NgayKetThuc.Value.Date;
                 query = query.Where(c => c.NgayKetThuc == NgayKetThuc);
             }
             if (TuNgay.HasValue)
             {
                 var _tungay = TuNgay.Value.Date;
                 if (isGDNhan)
                     query = query.Where(c => c.NgayNhanHang > _tungay);
                 else
                     query = query.Where(c => c.NgayKetThuc > _tungay);
             }
             if (DenNgay.HasValue)
             {
                 var _dengay = DenNgay.Value.Date.AddDays(1);
                 if (isGDNhan)
                     query = query.Where(c => c.NgayNhanHang < _dengay);
                 else
                     query = query.Where(c => c.NgayKetThuc < _dengay);
             }
             if (Thang > 0 && Nam > 0)
             {
                 query = query.Where(c => c.NgayNhanHang.Year == Nam && c.NgayNhanHang.Month == Thang);
             }
             if (vanphonggui_id>0)
                 query = query.Where(c => c.VanPhongGuiId == vanphonggui_id);

             if (VanPhongNhanId > 0)
             {

                 query = query.Where(c => c.VanPhongNhanId == VanPhongNhanId);
             }

             if (TuyenId > 0)
                 query = query.Where(c => c.nhatkyvanchuyens.FirstOrDefault().chuyendi.HanhTrinhId == TuyenId);
            

             if (!String.IsNullOrWhiteSpace(_thongtin))
             {
                 query = query.Where(m => (m.MaPhieu.Contains(_thongtin) || m.NguoiGui.HoTen.Contains(_thongtin) || m.NguoiNhan.HoTen.Contains(_thongtin) || m.TenHang.Contains(_thongtin)));
             }
            
             query = query.OrderByDescending(m => m.Id);
             return query.ToList();
         }
         public virtual List<PhieuChuyenPhat> GetPhieuTonVaThatLac(int NhaXeId,   string _thongtin = "", int VanPhongNhanId = 0,int TrangThaiId=0)
         {
             
             var arr = new int[] { (int)ENTrangThaiChuyenPhat.DangVanChuyen, (int)ENTrangThaiChuyenPhat.DaXepLenh };
             var query = _phieuchuyenphatRepository.Table.Where(m =>m.NhaXeId==NhaXeId && m.VanPhongNhanId==VanPhongNhanId );
                 //get hang ton
             if(TrangThaiId==(int)ENTrangThaiHangTrongKho.HangTon)
             {
                 query = query.Where(c => c.TrangThaiId == (int)ENTrangThaiChuyenPhat.DenVanPhongNhan && c.NgayDenVanPhongNhan.Value < DateTime.Now);
             }
             if (TrangThaiId == (int)ENTrangThaiHangTrongKho.HangThatLac)
             {
                 var _ngayhientai = DateTime.Now.AddDays(-2);
                 query = query.Where(c => arr.Contains(c.TrangThaiId) && c.NgayNhanHang < _ngayhientai);
             }
           
             if (!String.IsNullOrWhiteSpace(_thongtin))
             {
                 query = query.Where(m => (m.MaPhieu.Contains(_thongtin) || m.NguoiGui.HoTen.Contains(_thongtin) || m.NguoiNhan.HoTen.Contains(_thongtin) || m.TenHang.Contains(_thongtin)));
             }
             query = query.OrderByDescending(m => m.Id);
             return query.ToList();
         }
       
       
        private ENNhomPhieuChuyenPhat getNhomPhieuChuyenPhat(PhieuChuyenPhat item)
         {
            if(item.LoaiPhieu==ENLoaiPhieuChuyenPhat.ThuTaiVanPhong)
            {
                if (item.CuocVuotTuyen > 0)
                    return ENNhomPhieuChuyenPhat.VP_VT;
                if (item.CuocCapToc > 0)
                    return ENNhomPhieuChuyenPhat.VP_CT;
                if (item.CuocGiaTri > 0)
                    return ENNhomPhieuChuyenPhat.VP_GT;
                return ENNhomPhieuChuyenPhat.VP;
            }
            else
            {
                if (item.CuocVuotTuyen > 0)
                    return ENNhomPhieuChuyenPhat.TN_VT;
                if (item.CuocCapToc > 0)
                    return ENNhomPhieuChuyenPhat.TN_CT;
                if (item.CuocGiaTri > 0)
                    return ENNhomPhieuChuyenPhat.TN_GT;
                return ENNhomPhieuChuyenPhat.TN;
            }
         }
         public virtual void InsertPhieuChuyenPhat(PhieuChuyenPhat item)
         {
             if (item == null)
                 throw new ArgumentNullException("PhieuChuyenPhat");
             item.NgayTao = DateTime.Now;
             item.NgayUpdate = DateTime.Now;
             //tao thong tin nhom phieu
             item.NhomPhieu = getNhomPhieuChuyenPhat(item);
             _phieuchuyenphatRepository.Insert(item);
         }

         public virtual void UpdatePhieuChuyenPhat(PhieuChuyenPhat _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuChuyenPhat");
             _item.NgayUpdate = DateTime.Now;
             _item.NhomPhieu = getNhomPhieuChuyenPhat(_item);
             _phieuchuyenphatRepository.Update(_item);
         }
         public virtual void DeletePhieuChuyenPhat(PhieuChuyenPhat _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuChuyenPhat");
             _phieuchuyenphatRepository.Delete(_item);
         }
         public virtual PhieuChuyenPhat GetPhieuChuyenPhatById(int Id)
         {
             if (Id == 0)
                 throw new ArgumentNullException("PhieuChuyenPhat");
             return _phieuchuyenphatRepository.GetById(Id);
         }
        #endregion
         #region phieu chuyen phat nhat ky van chuyen
         public virtual PhieuChuyenPhatVanChuyen GetPhieuChuyenPhatVanChuyenById(int PhieuChuyenPhatId,int ChuyenDiId)
         {

             return _phieuchuyenphatvanchuyenRepository.Table.Where(c => c.PhieuChuyenPhatId == PhieuChuyenPhatId && c.ChuyenDiId == ChuyenDiId).FirstOrDefault();
         }
         public virtual PhieuChuyenPhatVanChuyen GetPhieuChuyenPhatVanChuyen(int PhieuChuyenPhatId, int PhieuVanChuyenId)
         {

             return _phieuchuyenphatvanchuyenRepository.Table.Where(c => c.PhieuChuyenPhatId == PhieuChuyenPhatId && c.PhieuVanChuyenId == PhieuVanChuyenId).FirstOrDefault();
         }
         public virtual void UpdatePhieuChuyenPhatVanChuyen(PhieuChuyenPhatVanChuyen _item)
         {
             if(_item.Id==0)
                _phieuchuyenphatvanchuyenRepository.Insert(_item);
             else
                 _phieuchuyenphatvanchuyenRepository.Update(_item);
         }
         public virtual void DeletePhieuChuyenPhatVanChuyen(int Id)
         {
             var _item = _phieuchuyenphatvanchuyenRepository.GetById(Id);
             if (_item == null)
                 return;
             _phieuchuyenphatvanchuyenRepository.Delete(_item);
         }
         public virtual List<PhieuChuyenPhatVanChuyen> GetAllPhieuChuyenPhatVanChuyen(DateTime NgayGuiHangTu, DateTime NgayGuiHangDen, int VanPhongGuiId = 0, int VanPhongNhanId = 0, string BienSoXe = "", string SoLenh = "", int TuyenId = 0)

         {
             NgayGuiHangDen = NgayGuiHangDen.Date.AddDays(1);
             var items = _phieuchuyenphatvanchuyenRepository.Table.Where(c => c.phieuchuyenphat.NgayNhanHang >= NgayGuiHangTu
               && c.phieuchuyenphat.NgayNhanHang < NgayGuiHangDen
                 );
             if (VanPhongGuiId > 0)
                 items = items.Where(c => c.VanPhongGuiId == VanPhongGuiId);
             if (VanPhongNhanId > 0)
                 items = items.Where(c => c.VanPhongNhanId == VanPhongNhanId);
             if (!String.IsNullOrWhiteSpace(SoLenh))
                 items = items.Where(m => m.phieuvanchuyen.SoLenh.Contains(SoLenh));
             if (!String.IsNullOrWhiteSpace(BienSoXe))
                 items = items.Where(m => m.chuyendi.xevanchuyen.BienSo.Contains(BienSoXe));
             if (TuyenId > 0)
                 items = items.Where(c => c.TuyenId == TuyenId);
             return items.ToList();
         }
        
         #endregion
        #region nhat ki phieu chuyen phat
         public virtual void InsertPhieuChuyenPhatLog(string GhiChu, int PhieuChuyenPhatId)
         {
             if (PhieuChuyenPhatId == 0)
                 throw new ArgumentNullException("PhieuChuyenPhatLog");
             var _item = new PhieuChuyenPhatLog();
             _item.PhieuChuyenPhatId = PhieuChuyenPhatId;
             _item.GhiChu = GhiChu;
             _item.NgayTao = DateTime.Now;
             _phieuchuyenphatlogRepository.Insert(_item);
         }
        #endregion
         #region nhat ki phieu van chuyen
         public virtual PhieuVanChuyenLog GetPhieuVanChuyenLog(int PhieuVanChuyenId, int ChuyenDiId)
         {
             var query = _phieuvanchuyenlogRepository.Table.Where(c => c.PhieuVanChuyenId == PhieuVanChuyenId && c.ChuyenDiId == ChuyenDiId).FirstOrDefault();
             return query;
         }
         public virtual void UpdatePhieuVanChuyenLog(PhieuVanChuyenLog _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuVanChuyenLog");
             if (_item.Id==0)
                _phieuvanchuyenlogRepository.Insert(_item);
             else
                 _phieuvanchuyenlogRepository.Update(_item);
         }
         public virtual void DeletePhieuVanChuyenLog(int Id)
         {
             var _item = _phieuvanchuyenlogRepository.GetById(Id);
             if (_item == null)
                 return;
             _phieuvanchuyenlogRepository.Delete(_item);
         }
         public List<PhieuVanChuyenLog> GetAllPhieuVanChuyenLog()
         {
             return null;
         }
         public PhieuVanChuyenLog GetPhieuVanChuyenLogById(int Id)
         {
             if (Id == 0)
                 throw new ArgumentNullException("PhieuChuyenPhat");
             return _phieuvanchuyenlogRepository.GetById(Id);
         }
         #endregion
        #region phieu van chuyen
         public virtual List<PhieuVanChuyen> GetAllPhieuVanChuyen(int NhaXeId, int VanPhongGuiId = 0, string SoLenh = "", ENTrangThaiPhieuVanChuyen TrangThaiId = ENTrangThaiPhieuVanChuyen.All, DateTime? ngaytao = null, int VanPhongNhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
         {
             var query = _phieuvanchuyenRepository.Table.Where(m => m.TrangThaiId != (int)ENTrangThaiChuyenPhat.Huy);
             query = query.Where(m => m.NhaXeId == NhaXeId);
             if (!String.IsNullOrWhiteSpace(SoLenh))
                 query = query.Where(m => m.SoLenh.Contains(SoLenh));

             if (VanPhongGuiId > 0)
             {
                 query = query.Where(m => (m.VanPhongId == VanPhongGuiId 
                     || m.nhatkyvanchuyens.Any(nk=>nk.VanPhongGuiId==VanPhongGuiId)
                     || (m.LoaiPhieuVanChuyenId==(int)ENLoaiPhieuVanChuyen.VuotTuyen && m.TrangThaiId==(int)ENTrangThaiPhieuVanChuyen.DenVanPhongNhan && m.nhatkyvanchuyens.Any(nk=>nk.VanPhongNhanId==VanPhongGuiId))
                     ));
             }
             if (VanPhongNhanId > 0)
             {
                 //co thiet lap nhan hang tu nhat ky, hoa trong phieu chuyen phat co thong tin van phong nhan(chi ap dung doi voi phieu van chuyen trong tuyen)
                 query = query.Where(m => m.nhatkyvanchuyens.Any(nk => nk.VanPhongNhanId == VanPhongNhanId)
                     || (
                        m.phieuchuyenphats.Any(p => p.VanPhongNhanId == VanPhongNhanId) 
                        && m.LoaiPhieuVanChuyenId==(int)ENLoaiPhieuVanChuyen.TrongTuyen
                        )                     
                     );
             }
             if (TrangThaiId > 0)
                 query = query.Where(m => m.TrangThaiId == (int)TrangThaiId);
             if (ngaytao.HasValue)
                 query = query.Where(c => c.NgayTao.Year == ngaytao.Value.Year
                     && c.NgayTao.Month == ngaytao.Value.Month
                     && c.NgayTao.Day == ngaytao.Value.Day);
             if (TuNgay.HasValue)
             {
                 var _tungay = TuNgay.Value.Date;
                 query = query.Where(c => c.NgayTao > _tungay);
             }
             if (DenNgay.HasValue)
             {
                 var _dengay = DenNgay.Value.Date.AddDays(1);
                 query = query.Where(c => c.NgayTao < _dengay);
             }

             query = query.OrderByDescending(m => m.Id);
             return query.ToList();

         }
         public virtual void InsertPhieuVanChuyen(PhieuVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuVanChuyen");
             _phieuvanchuyenRepository.Insert(_item);
         }
        public virtual   List<PhieuVanChuyen> GetAllPhieuVanChuyenByChuyenId(int ChuyenId,int NhaXeId)
         {
             var query = _phieuvanchuyenRepository.Table.Where(m => m.TrangThaiId != (int)ENTrangThaiChuyenPhat.Huy);
             query = query.Where(m => m.NhaXeId == NhaXeId 
                 && m.nhatkyvanchuyens.Any(nk=>nk.ChuyenDiId==ChuyenId));

             return query.ToList();
         }
         public virtual void UpdatePhieuVanChuyen(PhieuVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuVanChuyen");

             _phieuvanchuyenRepository.Update(_item);
         }
         public virtual void DeletePhieuVanChuyen(PhieuVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("PhieuVanChuyen");
             _phieuvanchuyenRepository.Delete(_item);
         }
         public virtual PhieuVanChuyen GetPhieuVanChuyenById(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException("PhieuVanChuyen");
             return _phieuvanchuyenRepository.GetById(id);
         }
         public virtual int GetSoLenhVanChuyenTiepTheo(int NhaXeId, int VanPhongId)
         {
             var ngaytao = DateTime.Now.Date;
             var query = _phieuvanchuyenRepository.Table.Where(m => m.TrangThaiId != (int)ENTrangThaiChuyenPhat.Huy && m.NgayTao>ngaytao);
             query = query.Where(m => m.NhaXeId == NhaXeId && m.VanPhongId == VanPhongId).OrderByDescending(m=>m.SoLenhNum);
             var item = query.FirstOrDefault();
             if (item != null)
                 return item.SoLenhNum + 1;
             return 1;
         }
       #endregion
         #region khach hang
         public virtual PagedList<KhachHang> GetAllKhachHang(int NhaXeId = 0,
               int pageIndex = 0,
               int pageSize = int.MaxValue)
         {
             var query = _khachhangRepository.Table.Where(m => m.NhaXeId == NhaXeId);

             query = query.OrderByDescending(m => m.Id);
             return new PagedList<KhachHang>(query, pageIndex, pageSize);

         }
         public virtual List<KhachHang> GetAllKhachHang(int NhaXeId, string KeySearch)
         {
             var query = _khachhangRepository.Table.Where(m => m.NhaXeId == NhaXeId);
             if(!string.IsNullOrEmpty(KeySearch))
             {
                 query = query.Where(c => c.SoDienThoai.Contains(KeySearch));
             }
             return query.Take(10).ToList();
         }
         public virtual void InsertKhachHang(KhachHang _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhachHang");
             _khachhangRepository.Insert(_item);
         }

         public virtual void UpdateKhachHang(KhachHang _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhachHang");
             _khachhangRepository.Update(_item);
         }
         public virtual void DeleteKhachHang(KhachHang _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhachHang");
             _khachhangRepository.Delete(_item);
         }
         public virtual KhachHang GetKhachHangById(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException("KhachHang");
             return _khachhangRepository.GetById(id);
         }
         #endregion
         #region khu vuc
         public virtual List<KhuVuc> GetAllKhuVuc(int NhaXeId = 0)
         {
             var query = _khuvucRepository.Table.Where(m => m.NhaXeId == NhaXeId);

             query = query.OrderBy(m => m.Id);
             return query.ToList();

         }
         public virtual void InsertKhuVuc(KhuVuc _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhuVuc");
             _khuvucRepository.Insert(_item);
         }

         public virtual void UpdateKhuVuc(KhuVuc _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhuVuc");
             _khuvucRepository.Update(_item);
         }
         public virtual void DeleteKhuVuc(KhuVuc _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("KhuVuc");
             _khuvucRepository.Delete(_item);
         }
         public virtual KhuVuc GetKhuVucById(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException("KhuVuc");
             return _khuvucRepository.GetById(id);
         }
         #endregion
         #region To van chuyen, tovan chuyen van phong
         public virtual ToVanChuyenVanPhong GetToVanChuyenVanPhong(int VanPhongId,int ToVanChuyenId)
         {

             var query = _tovanchuyenvanphongRepository.Table.Where(c => c.ToVanChuyenId == ToVanChuyenId && c.VanPhongId == VanPhongId);
             return query.ToList().FirstOrDefault();
         }
         public virtual void InsertToVanChuyenVanPhong(ToVanChuyenVanPhong _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyenVanPhong");
             _tovanchuyenvanphongRepository.Insert(_item);
         }

         public virtual void UpdateToVanChuyenVanPhong(ToVanChuyenVanPhong _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyenVanPhong");
             _tovanchuyenvanphongRepository.Update(_item);
         }
         public virtual void DeleteToVanChuyenVanPhong(ToVanChuyenVanPhong _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyenVanPhong");
             _tovanchuyenvanphongRepository.Delete(_item);
         }
         public virtual ToVanChuyenVanPhong GetToVanChuyenVanPhongById(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException("ToVanChuyenVanPhong");
             return _tovanchuyenvanphongRepository.GetById(id);
         }
         public virtual List<ToVanChuyen> GetAllToVanChuyen(int NhaXeId)
         {
             var query = _tovanchuyenRepository.Table.Where(c=>c.NhaXeId==NhaXeId);
             return query.ToList();

         }
       
         public virtual List<VanPhong> GetAllVanPhongByToVanChuyen(int ToVanChuyenId)
         {
             var query = _tovanchuyenvanphongRepository.Table.Where(c => c.ToVanChuyenId == ToVanChuyenId);

             return query.ToList().Select(c=>c.vanphong).ToList();

         }
         public virtual void InsertToVanChuyen(ToVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _tovanchuyenRepository.Insert(_item);
         }

         public virtual void UpdateToVanChuyen(ToVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _tovanchuyenRepository.Update(_item);
         }
         public virtual void DeleteToVanChuyen(ToVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _tovanchuyenRepository.Delete(_item);
         }
         public virtual ToVanChuyen GetToVanChuyenById(int id)
         {
             if (id == 0)
                 throw new ArgumentNullException("ToVanChuyen");
             return _tovanchuyenRepository.GetById(id);
         }

         public virtual List<NguoiVanChuyen> GetAllNguoiVanChuyen(int ToVanChuyenId)
         {
             var query = _nguoivanchuyenRepository.Table.Where(c => c.ToVanChuyenId == ToVanChuyenId);
             return query.ToList();

         }
         public virtual void InsertNguoiVanChuyen(NguoiVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _nguoivanchuyenRepository.Insert(_item);
         }

         public virtual void UpdateNguoiVanChuyen(NguoiVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _nguoivanchuyenRepository.Update(_item);
         }
         public virtual void DeleteNguoiVanChuyen(NguoiVanChuyen _item)
         {
             if (_item == null)
                 throw new ArgumentNullException("ToVanChuyen");
             _nguoivanchuyenRepository.Delete(_item);
         }
         public virtual NguoiVanChuyen GetNguoiVanChuyenById(int Id)
         {
             if (Id == 0)
                 throw new ArgumentNullException("ToVanChuyen");
             return _nguoivanchuyenRepository.GetById(Id);
         }
         #endregion
         
        #region van phong vuot tuyen
         public virtual VanPhongVuotTuyen GetVanPhongVuotTuyenByVanPhongNhan(int VanPhongGuiId, int VanPhongCuoiId)
         {
             if (VanPhongGuiId == 0|| VanPhongCuoiId==0)
                 throw new ArgumentNullException("VanPhongVuotTuyen");
             var query = _vanphongvuottuyenRepository.Table.Where(m => m.VanPhongGuiId==VanPhongGuiId && m.VanPhongCuoiId==VanPhongCuoiId);
             return query.FirstOrDefault();
         }
         public virtual List<VanPhongVuotTuyen> GetVanPhongVuotTuyenByVanPhong(int VanPhongGuiId=0, int VanPhongCuoiId=0)
         {
            
             var query = _vanphongvuottuyenRepository.Table;
             if(VanPhongGuiId>0)
             {
                 query = query.Where(c => c.VanPhongGuiId == VanPhongGuiId);
             }
             if (VanPhongCuoiId > 0)
             {
                 query = query.Where(c => c.VanPhongCuoiId == VanPhongCuoiId);
             }
             return query.ToList();
         }
         public virtual List<HistoryXeXuatBen> GetAllChuyenDi(int NhaXeId, int VanPhongGuiId, int VanPhongNhanId,DateTime NgayDi)
         {
             var retcds = new List<HistoryXeXuatBen>();
             var query = _chuyendiRepository.Table.Where(c => c.HanhTrinh.NhaXeId == NhaXeId && c.NgayDi >= NgayDi&& c.TrangThaiId!=(int)ENTrangThaiXeXuatBen.HUY);
             //lay thong tin diem don co van phong nay
             //cac diem don bat buoc phai co vanphong
             var diemguiquery = _diemdonRepository.Table.Where(c => c.NhaXeId == NhaXeId && c.VanPhongId == VanPhongGuiId).FirstOrDefault();
             //neu ko co thi empty, loi cau hinh diem don van phong
             if (diemguiquery == null)
                 return retcds;
             var diemnhanquery = _diemdonRepository.Table.Where(c => c.NhaXeId == NhaXeId && c.VanPhongId == VanPhongNhanId).FirstOrDefault();
             //neu ko co thi empty, loi cau hinh diem don van phong
             if (diemnhanquery == null)
                 return retcds;
             //lay tat ca chuyen di co di qua diem don nay
             query = query.Where(c => c.HanhTrinh.DiemDons.Any(h => h.DiemDonId == diemguiquery.Id) && c.HanhTrinh.DiemDons.Any(h => h.DiemDonId == diemnhanquery.Id));
             //phai lay duoc chuyen di di tu VanPhongGuiId-> VanPhongNhanId
             var chuyendis = query.OrderByDescending(c => c.NgayDi).ToList();
             foreach(var cd in chuyendis)
             {
                 int sstdiemgui = cd.HanhTrinh.DiemDons.Where(d => d.DiemDonId == diemguiquery.Id).First().ThuTu;
                 int sstdiemnhan = cd.HanhTrinh.DiemDons.Where(d => d.DiemDonId == diemnhanquery.Id).First().ThuTu;
                 if(sstdiemgui<sstdiemnhan)
                 {
                     retcds.Add(cd);
                 }
             }
             return retcds;
         }
         public virtual List<VanPhong> GetAllVanPhongByVanPhongId(int NhaXeId, int VanPhongId)
         {
             var vanphongs = new List<VanPhong>();
             var diemdonquery = _diemdonRepository.Table.Where(c => c.NhaXeId == NhaXeId && c.VanPhongId == VanPhongId).FirstOrDefault();
             if (diemdonquery == null)
                 return vanphongs;

             var hanhtrinhs = _hanhtrinhRepository.Table.Where(c => c.NhaXeId == NhaXeId && c.DiemDons.Any(d => d.DiemDonId == diemdonquery.Id)).ToList();
             foreach(var ht in hanhtrinhs)
             {
                 foreach(var dd in ht.DiemDons)
                 {
                     if(dd.diemdon!=null && !vanphongs.Any(c=>c.Id==dd.diemdon.VanPhongId))
                     {
                         vanphongs.Add(dd.diemdon.vanphong);
                     }
                 }
             }

             return vanphongs;

         }
        #endregion
    }
}
