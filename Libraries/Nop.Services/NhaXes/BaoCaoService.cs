using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using Nop.Core.Domain.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Seo;
using Nop.Core.Domain.Directory;
using Nop.Data;



namespace Nop.Services.NhaXes
{
    public partial class BaoCaoService:IBaoCaoService
    {
        private readonly IRepository<VeXeItem> _vexeitemRepository;
        private readonly IRepository<MenhGiaVe> _menhgiaveRepository;
        private readonly IRepository<NguonVeXe> _nguonvexeRepository;
        private readonly IRepository<HistoryXeXuatBen> _hisRepository;

        private readonly IRepository<GiaoDichKeVe> _giaodichkeveRepository;
        private readonly IRepository<GiaoDichKeVeMenhGia> _giaodichkevemenhgiaRepository;
        private readonly IRepository<GiaoDichKeVeXeItem> _giaodichkevexeitemRepository;
        private readonly IRepository<QuanLyMauVeKyHieu> _quanlymauvekyhieuRepository;
        private readonly IRepository<HanhTrinhGiaVe> _hanhtrinhgiaveRepository;
        private readonly IDbContext _dbContext;
        private readonly IRepository<NhanVien> _nhanvienRepository;
        private readonly IRepository<PhoiVe> _phoiveRepository;
        private readonly IRepository<VeXeQuyen> _vexequyenRepository;
        private readonly IRepository<XeXuatBenVeXeItem> _xexuatbenvexeitemRepository;
        public BaoCaoService(IRepository<XeXuatBenVeXeItem> xexuatbenvexeitemRepository,
            IRepository<VeXeQuyen> vexequyenRepository,
            IRepository<VeXeItem> vexeitemRepository,
            IRepository<MenhGiaVe> menhgiaveRepository,
             IRepository<NguonVeXe> nguonvexeRepository,
             IRepository<HistoryXeXuatBen> hisRepository,
            IRepository<GiaoDichKeVe> giaodichkeveRepository,
            IRepository<GiaoDichKeVeMenhGia> giaodichkevemenhgiaRepository,
            IRepository<GiaoDichKeVeXeItem> giaodichkevexeitemRepository,
            IRepository<QuanLyMauVeKyHieu> quanlymauvekyhieuRepository,
            IRepository<HanhTrinhGiaVe> hanhtrinhgiaveRepository,
             IRepository<NhanVien> nhanvienRepository,
            IDbContext dbContext,
            IRepository<PhoiVe> phoiveRepository
            )
        {
            this._xexuatbenvexeitemRepository = xexuatbenvexeitemRepository;
            this._vexequyenRepository = vexequyenRepository;
            this._phoiveRepository = phoiveRepository;
            this._dbContext = dbContext;
            this._hanhtrinhgiaveRepository = hanhtrinhgiaveRepository;
            this._vexeitemRepository = vexeitemRepository;
            this._hisRepository = hisRepository;
            this._menhgiaveRepository = menhgiaveRepository;
            this._nguonvexeRepository = nguonvexeRepository;
            this._giaodichkeveRepository = giaodichkeveRepository;
            this._giaodichkevemenhgiaRepository = giaodichkevemenhgiaRepository;
            this._giaodichkevexeitemRepository = giaodichkevexeitemRepository;
            this._quanlymauvekyhieuRepository = quanlymauvekyhieuRepository;
            this._nhanvienRepository = nhanvienRepository;
        }
        #region Xe xuat ben
        public virtual List<HistoryXeXuatBen> GetXeXuatBens(int NhaXeId, int XeVanChuyenId = 0, int[] hanhtrinhIds = null, int[] laiphuxeids = null, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            if (TuNgay.HasValue)
                TuNgay = TuNgay.Value.Date;
            if (DenNgay.HasValue)
                DenNgay = DenNgay.Value.Date;

            var query = _hisRepository.Table.Where(c => c.TrangThaiId != (int)ENTrangThaiXeXuatBen.HUY);
            query = query.Where(c => c.HanhTrinh.NhaXeId == NhaXeId);
            if(XeVanChuyenId>0)
                query = query.Where(c => c.XeVanChuyenId == XeVanChuyenId);
            else
                //lay cac chuyen da thiet dat xe chay
                if (XeVanChuyenId<0)
                    query = query.Where(c => c.XeVanChuyenId >0);
            if (hanhtrinhIds != null)
                query = query.Where(c => hanhtrinhIds.Contains(c.HanhTrinhId));
            if (laiphuxeids!=null)
            {
                query = query.Where(c =>c.LaiPhuXes.Any(m=>laiphuxeids.Contains(m.NhanVien_Id)));
            }
            if(TuNgay.HasValue)
            {
                query = query.Where(c => c.NgayDi >= TuNgay.Value);
            }
            if (DenNgay.HasValue)
            {
                var _dengay = DenNgay.Value;
                //neu den ngay khong co gio va phut thi add them 1 ngay
                if(_dengay.Hour==0 && _dengay.Minute==0)
                    _dengay = _dengay.AddDays(1);
                else // nguoc lai add them 1 phut
                    _dengay = _dengay.AddMinutes(1);
                query = query.Where(c => c.NgayDi < _dengay);
            }
            return query.OrderBy(c=>c.NgayDi).ToList();
                
        }        
        #endregion
        #region Phoi ve
        public virtual Decimal GetTongDoanhThuChuyenDi(int[] NguoVeIds, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            if (TuNgay.HasValue)
                TuNgay = TuNgay.Value.Date;
            if (DenNgay.HasValue)
                DenNgay = DenNgay.Value.Date;

            var trangthaiids=new int[] { (int)ENTrangThaiPhoiVe.KetThuc,(int)ENTrangThaiPhoiVe.DaThanhToan,(int)ENTrangThaiPhoiVe.DaGiaoHang};
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && NguoVeIds.Contains(c.NguonVeXeId));
            if (TuNgay.HasValue)
            {
                query = query.Where(c => c.NgayDi >= TuNgay.Value);
            }
            if (DenNgay.HasValue)
            {
                var _dengay = DenNgay.Value.AddDays(1);
                query = query.Where(c => c.NgayDi < _dengay);
            }
            if(query.Any())
                return query.Sum(c => c.GiaVeHienTai);
            return decimal.Zero;
        }
        public Decimal GetTongDoanhThuTheoChuyenDi(int[] ChuyenDiIds)
        {
            var trangthaiids = new int[] { (int)ENTrangThaiPhoiVe.KetThuc, (int)ENTrangThaiPhoiVe.DaThanhToan, (int)ENTrangThaiPhoiVe.DaGiaoHang };
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && c.ChuyenDiId>0 && ChuyenDiIds.Contains(c.ChuyenDiId.Value));
            if (query.Any())
                return query.Sum(c => c.GiaVeHienTai);
            return decimal.Zero;
        }
        public int GetTongDoanhThuChangTheoChuyenDi(int ChangId, int[] ChuyenDiIds)
        {
            var trangthaiids = new int[] { (int)ENTrangThaiPhoiVe.KetThuc, (int)ENTrangThaiPhoiVe.DaThanhToan, (int)ENTrangThaiPhoiVe.DaGiaoHang };
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && c.ChuyenDiId > 0 && ChuyenDiIds.Contains(c.ChuyenDiId.Value) && c.ChangId == ChangId);
            if (query.Any())
                return query.Count();
            return 0;
        }
        public decimal GetTongDoanhThuChangTheoTuyen(int ChangId, int[] ChuyenDiIds)
        {
            var trangthaiids = new int[] { (int)ENTrangThaiPhoiVe.KetThuc, (int)ENTrangThaiPhoiVe.DaThanhToan, (int)ENTrangThaiPhoiVe.DaGiaoHang };
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && c.ChuyenDiId > 0 && ChuyenDiIds.Contains(c.ChuyenDiId.Value) && c.ChangId == ChangId);
            if (query.Any())
                return query.Sum(c=>c.GiaVeHienTai);
            return 0;
        }
        public int GetSoKhachLenTaiDiemTheoChuyenDi(int DiemId, int ChuyenId)
        {
            var trangthaiids = new int[] { (int)ENTrangThaiPhoiVe.KetThuc, (int)ENTrangThaiPhoiVe.DaThanhToan, (int)ENTrangThaiPhoiVe.DaGiaoHang };
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && c.ChuyenDiId > 0 && c.ChuyenDiId.Value == ChuyenId).ToList();
            var khachlen = query.Where(c => c.changgiave.DiemDonId == DiemId);
            if (khachlen.Any())
                return khachlen.Count();
            return 0;
        }
        public int GetSoKhachXuongTaiDiemTheoChuyenDi(int DiemId, int ChuyenId)
        {
            var trangthaiids = new int[] { (int)ENTrangThaiPhoiVe.KetThuc, (int)ENTrangThaiPhoiVe.DaThanhToan, (int)ENTrangThaiPhoiVe.DaGiaoHang };
            var query = _phoiveRepository.Table.Where(c => trangthaiids.Contains(c.TrangThaiId) && c.ChuyenDiId > 0 && c.ChuyenDiId.Value == ChuyenId).ToList();
            var khachlen = query.Where(c => c.changgiave.DiemDenId == DiemId);
            if (khachlen.Any())
                return khachlen.Count();
            return 0;
        }
        #endregion
    }
}
