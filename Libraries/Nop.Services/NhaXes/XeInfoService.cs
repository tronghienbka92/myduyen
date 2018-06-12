using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public class XeInfoService:IXeInfoService
    {
        #region "khai bao"
        private readonly IRepository<LoaiXe> _tableRepository;
        private readonly IRepository<SoDoGheXe> _sodoghexeRepository;
        private readonly IRepository<HistoryXeXuatBen> _historyxexuatbenRepository;
        private readonly IRepository<SoDoGheXeViTri> _sodoghexevitriRepository;
        private readonly IRepository<GheItem> _gheitemRepository;
        private readonly IRepository<SoDoGheXeQuyTac> _sodoghexequytacRepository;
        private readonly IRepository<XeVanChuyen> _xevanchuyenRepository;

        public XeInfoService(IRepository<LoaiXe> tableRepository,
            IRepository<GheItem> gheitemRepository,
            IRepository<SoDoGheXe> sodoghexeRepository,
              IRepository<HistoryXeXuatBen> historyxexuatbenRepository,
            IRepository<SoDoGheXeViTri> sodoghexevitriRepository,
            IRepository<SoDoGheXeQuyTac> sodoghexequytacRepository,
            IRepository<XeVanChuyen> xevanchuyenRepository
            )
        {
            this._tableRepository = tableRepository;
            this._gheitemRepository = gheitemRepository;
            this._sodoghexeRepository = sodoghexeRepository;
            this._historyxexuatbenRepository = historyxexuatbenRepository;
            this._sodoghexevitriRepository = sodoghexevitriRepository;
            this._sodoghexequytacRepository = sodoghexequytacRepository;
            this._xevanchuyenRepository = xevanchuyenRepository;
        }
        #endregion
        #region so do ghe
        public virtual List<SoDoGheXe> GetAllSoDoGheXe(int KieuXeId)
        {
            var query = _sodoghexeRepository.Table;
            if (KieuXeId>0)
                query = query.Where(c => c.KieuXeId == KieuXeId);
            return query.ToList();            
        }
        public virtual SoDoGheXe GetSoDoGheXeById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _sodoghexeRepository.GetById(itemId);
        }
        public virtual List<SoDoGheXeViTri> GetAllSoDoGheViTri(int SoDoGheXeId)
        {
            var query = _sodoghexevitriRepository.Table;
            query = query.Where(c => c.SoDoGheXeId == SoDoGheXeId);
            return query.ToList();
        }
        public virtual SoDoGheXeViTri GetSoDoGheXeViTri(int SoDoGheXeId, int x, int y)
        {
            var query = _sodoghexevitriRepository.Table;
            query = query.Where(c => c.SoDoGheXeId == SoDoGheXeId && c.x==x && c.y==y);
            return query.FirstOrDefault();
        }
        #endregion
        #region Loai Xe

        public virtual PagedList<LoaiXe> GetAll(int NhaXeId = 0, string tenLoaiXe = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue)
        {
            var query = _tableRepository.Table;
            if (!String.IsNullOrWhiteSpace(tenLoaiXe))
                query = query.Where(m => m.TenLoaiXe.Contains(tenLoaiXe));
            query = query.Where(m => m.NhaXeId == NhaXeId);

            query = query.OrderBy(m => m.Id);
            return new PagedList<LoaiXe>(query, pageIndex, pageSize);
        }
        public virtual List<LoaiXe> GetAllByNhaXeId(int NhaXeId)
        {
            var query = _tableRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            return query.ToList();
        }
        public virtual LoaiXe GetById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _tableRepository.GetById(itemId);
        }
        public virtual void Insert(LoaiXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LoaiXe");
            _tableRepository.Insert(_item);
        }
        public virtual void Update(LoaiXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LoaiXe");

            _tableRepository.Update(_item);
        }
        public virtual void Delete(LoaiXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("LoaiXe");
            DeleteGheAndSoDoGheXeQuyTac(_item.Id);
            _tableRepository.Delete(_item);
        }
        #endregion
        #region Ghe xe
        //_gheitemRepository
        public virtual List<GheItem> GetAllGheItem(int LoaiXeId)
        {
            var query = _gheitemRepository.Table;
            query = query.Where(m => m.LoaiXeId == LoaiXeId);
            return query.ToList();
        }
        public virtual GheItem GetGheItemById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _gheitemRepository.GetById(itemId);
        }
        public virtual void InsertGheItem(GheItem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("GheItem");
            _gheitemRepository.Insert(_item);
        }
        public virtual void UpdateGheItem(GheItem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("GheItem");

            _gheitemRepository.Update(_item);
        }
        public virtual void DeleteGheItem(GheItem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("GheItem");
            _gheitemRepository.Delete(_item);
        }
        #endregion
        #region So do ghe quy tac
        //_sodoghexequytacRepository
        public virtual List<SoDoGheXeQuyTac> GetAllSoDoGheXeQuyTac(int LoaiXeId)
        {
            var query = _sodoghexequytacRepository.Table;
            query = query.Where(m => m.LoaiXeId == LoaiXeId);
            return query.ToList();
        }
        public virtual SoDoGheXeQuyTac GetSoDoGheXeQuyTacById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _sodoghexequytacRepository.GetById(itemId);
        }
        public virtual void InsertSoDoGheXeQuyTac(SoDoGheXeQuyTac _item)
        {
            if (_item == null)
                throw new ArgumentNullException("SoDoGheXeQuyTac");
            _sodoghexequytacRepository.Insert(_item);
        }
        public virtual void UpdateSoDoGheXeQuyTac(SoDoGheXeQuyTac _item)
        {
            if (_item == null)
                throw new ArgumentNullException("SoDoGheXeQuyTac");

            _sodoghexequytacRepository.Update(_item);
        }
        public virtual void DeleteSoDoGheXeQuyTac(SoDoGheXeQuyTac _item)
        {
            if (_item == null)
                throw new ArgumentNullException("SoDoGheXeQuyTac");
            _sodoghexequytacRepository.Delete(_item);
        }
        public virtual void DeleteGheAndSoDoGheXeQuyTac(int LoaiXeId)
        {
            var sdgquytac=_sodoghexequytacRepository.Table.Where(c=>c.LoaiXeId==LoaiXeId).ToList();
            _sodoghexequytacRepository.Delete(sdgquytac);
            var gheitems = _gheitemRepository.Table.Where(c => c.LoaiXeId == LoaiXeId).ToList();
            _gheitemRepository.Delete(gheitems);
        }
        #endregion
        #region Thong tin Xe

        public virtual PagedList<XeVanChuyen> GetAllXeInfo(int NhaXeId = 0, string tenxe = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue)
        {
            var query = _xevanchuyenRepository.Table;
            if (!String.IsNullOrWhiteSpace(tenxe))
                query = query.Where(m => m.TenXe.Contains(tenxe));
            query = query.Where(m => m.NhaXeId == NhaXeId);
            query = query.Where(m => m.TrangThaiXeId != (int)ENTrangThaiXe.Huy);
            query = query.OrderBy(m => m.Id);
            return new PagedList<XeVanChuyen>(query, pageIndex, pageSize);
        }
        public virtual List<XeVanChuyen> GetAllXeInfoByNhaXeId(int NhaXeId)
        {
            var query = _xevanchuyenRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            query = query.Where(m => m.TrangThaiXeId != (int)ENTrangThaiXe.Huy);
            return query.ToList();
        }
        public virtual List<XeVanChuyen> GetAllXeInfoByLoaiXeId(int LoaiXeId)
        {
            var query = _xevanchuyenRepository.Table;
            query = query.Where(m => m.LoaiXeId == LoaiXeId);
            query = query.Where(m => m.TrangThaiXeId != (int)ENTrangThaiXe.Huy);
            return query.ToList();
        }
        public virtual XeVanChuyen GetXeInfoById(int itemId)
        {
            if (itemId == 0)
                return null;
            var item= _xevanchuyenRepository.GetById(itemId);
            if (item != null && item.TrangThaiXeId != (int)ENTrangThaiXe.Huy)
                return item;
            return null;

        }
        public virtual XeVanChuyen GetXeInfoByBienSo(int NhaXeId,string BienSoXe)
        {
            if (NhaXeId == 0)
                return null;
            var query = _xevanchuyenRepository.Table.Where(c =>c.TrangThaiXeId!= (int)ENTrangThaiXe.Huy && c.NhaXeId == NhaXeId && c.BienSo == BienSoXe);
            return query.FirstOrDefault();
        }
        public virtual void InsertXeInfo(XeVanChuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("XeVanChuyen");
            _xevanchuyenRepository.Insert(_item);
        }
        public virtual void UpdateXeInfo(XeVanChuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("XeVanChuyen");

            _xevanchuyenRepository.Update(_item);
        }
        public virtual void DeleteXeInfo(XeVanChuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("XeVanChuyen");
            _item.TrangThaiXe = ENTrangThaiXe.Huy;
            UpdateXeInfo(_item);            
        }
        #endregion
        #region Dinh vi xe
        public virtual HistoryXeXuatBen DinhVi_GetHistoryXeXuatBenByXeVanChuyen(int XeVanChuyenId)
        {
            var _ngaydi=DateTime.Now;
            var query = _historyxexuatbenRepository.Table.Where(m => m.XeVanChuyenId == XeVanChuyenId
                && m.NgayDi.Year == _ngaydi.Year 
                && m.NgayDi.Month==_ngaydi.Month
                && m.NgayDi.Day==_ngaydi.Day
                && (m.TrangThaiId == (int)ENTrangThaiXeXuatBen.DANG_DI|| m.TrangThaiId==(int)ENTrangThaiXeXuatBen.CHO_XUAT_BEN));
            
            if (query.Count() > 0)
            {
                query = query.OrderByDescending(c => c.Id);
                var item = query.First();
                return item;
            }
            else
                return null;
                

            
        }
        #endregion

        public virtual List<XeVanChuyen> GetAllXeVanChuyenByNhaXeId(int NhaXeID)
        {
            var query = _xevanchuyenRepository.Table.Where(c => c.NhaXeId == NhaXeID).ToList();
            return query;
        }

        public virtual List<HistoryXeXuatBen> GetAllByXeVanChuyenID(int XeVanChuyenID)
        {
            var query = _historyxexuatbenRepository.Table.Where(c => c.XeVanChuyenId == XeVanChuyenID).ToList();
            return query;
        }
    }
}
