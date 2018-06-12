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


namespace Nop.Services.NhaXes
{
    public class KeToanService : IKeToanService
    {
        #region Ctor

        private readonly IRepository<ThuChi> _thuchiRepository;
        private readonly IRepository<LoaiThuChi> _loaithuchiRepository;

        public KeToanService(IRepository<ThuChi> thuchiRepository,
           IRepository<LoaiThuChi> loaithuchiRepository
           )
        {
            this._thuchiRepository = thuchiRepository;
            this._loaithuchiRepository = loaithuchiRepository;

        }
        #endregion
        #region Thu chi
        public List<ThuChi> GetAllThuChi(int NhaXeId, int LoaiThuChiId, bool? isChi, DateTime? tuNgay, DateTime? denNgay, string KeySearch)
        {
            var query = _thuchiRepository.Table.Where(c => c.NhaXeId == NhaXeId);
            if(LoaiThuChiId>0)
            {
                query = query.Where(c => c.LoaiThuChiId == LoaiThuChiId);
            }
            if (isChi.HasValue)
            {
                query = query.Where(c => c.isChi == isChi.Value);
            }
            if (tuNgay.HasValue)
            {
                var _tungay = tuNgay.Value.Date;
                query = query.Where(c => c.NgayGiaoDich >= _tungay);
            }
            if (denNgay.HasValue)
            {
                var _denngay = denNgay.Value.Date.AddDays(1);
                query = query.Where(c => c.NgayGiaoDich < _denngay);
            }
            if(!String.IsNullOrEmpty(KeySearch))
            {
                query = query.Where(c => (c.DienGiai.Contains(KeySearch) || (c.ChuyenDiId>0 && c.chuyendi.XeVanChuyenId>0 && c.chuyendi.xevanchuyen.BienSo.Contains(KeySearch))));
            }
            return query.OrderBy(c => c.NgayGiaoDich).ThenBy(c => c.Id).ToList();
        }

        public virtual void Insert(ThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _thuchiRepository.Insert(_item);
            

        }
        public virtual void Update(ThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _thuchiRepository.Update(_item);
        }
        public virtual void Delete(ThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _thuchiRepository.Delete(_item);
        }
        public virtual ThuChi GetThuChiById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("ThuChi");
            return _thuchiRepository.GetById(id);
        }
        public virtual ThuChi GetThuChiByCode(string Ma)
        {
            return _thuchiRepository.Table.Where(c => c.Ma == Ma).FirstOrDefault();
        }
        public virtual decimal GetTonDauKy(int NhaXeId, DateTime denNgay)
        {
            decimal ret=decimal.Zero;
            var tongthu = _thuchiRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isChi  && c.NgayGiaoDich < denNgay);
            if(tongthu.Any())
            {
                ret = tongthu.Sum(c => c.GiaTri);
            }
            var tongchi = _thuchiRepository.Table.Where(c => c.NhaXeId == NhaXeId && c.isChi && c.NgayGiaoDich < denNgay);
            if (tongchi.Any())
            {
                ret =ret- tongchi.Sum(c => c.GiaTri);
            }
            return ret;
        }
        public virtual void DeleteThuChiChuyenDi(int ChuyenDiId)
        {
            var phieuthuchis = _thuchiRepository.Table.Where(c => c.ChuyenDiId == ChuyenDiId).ToList();
            _thuchiRepository.Delete(phieuthuchis);
        }
        #endregion
        #region loai thu chi
        //loai thu chi
        public virtual List<LoaiThuChi> GetAllLoaiThuChi(int NhaXeId)
        {
            var query = _loaithuchiRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            return query.ToList();
        }

        public virtual void Insert(LoaiThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _loaithuchiRepository.Insert(_item);
        }
        public virtual void Update(LoaiThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _loaithuchiRepository.Update(_item);
        }
        public virtual void Delete(LoaiThuChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("ThuChi");
            _loaithuchiRepository.Delete(_item);
        }
        public virtual LoaiThuChi GetLoaiThuChiById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("ThuChi");
            return _loaithuchiRepository.GetById(id);
        }
        #endregion
    }
}
