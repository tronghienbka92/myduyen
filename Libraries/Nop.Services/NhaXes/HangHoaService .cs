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
    public class HangHoaService  : IHangHoaService 
    {
        #region Ctor
       
         private readonly IRepository<HangHoa> _hanghoaRepository;
         private readonly IRepository<LoaiHangHoa> _loaihanghoaRepository;
         private readonly IRepository<BangGiaCuoc> _banggiacuocRepository;

        public HangHoaService(IRepository<HangHoa> hanghoaRepository,
            IRepository<LoaiHangHoa> loaihanghoaRepository,
            IRepository<BangGiaCuoc> banggiacuocRepository
            )
        {
            this._banggiacuocRepository = banggiacuocRepository;
            this._loaihanghoaRepository = loaihanghoaRepository;
            this._hanghoaRepository = hanghoaRepository;
           
        }
        #endregion
        #region hàng hóa  
        public virtual List<HangHoa> GetAllHangHoaByPhieuGuiHangId(int phieuguihangid)
        {
            var query = _hanghoaRepository.Table;
            query = query.Where(m => m.PhieuGuiHangId == phieuguihangid);
            return query.ToList();
        }
       
        public  virtual void InsertHangHoa(HangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _hanghoaRepository.Insert(_item);
        }

        public virtual void UpdateHangHoa(HangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _hanghoaRepository.Update(_item);
        }
        public virtual void DeleteHangHoa(HangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _hanghoaRepository.Delete(_item);
        }
        public virtual HangHoa GetHangHoaById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("HangHoa");
          return  _hanghoaRepository.GetById(id);
        }
        #endregion
        #region Loai Hang Hoa
        public virtual List<LoaiHangHoa> GetAllLoaiHangHoa(int NhaXeId)
        {
            var query = _loaihanghoaRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            return query.ToList();
        }

        public virtual void Insert(LoaiHangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _loaihanghoaRepository.Insert(_item);
        }
        public virtual void Update(LoaiHangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _loaihanghoaRepository.Update(_item);
        }
        public virtual void Delete(LoaiHangHoa _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _loaihanghoaRepository.Delete(_item);
        }
        public virtual LoaiHangHoa GetLoaiHangHoaById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("HangHoa");
            return _loaihanghoaRepository.GetById(id);
        }
        #endregion
        #region BangGiaCuoc
        public virtual List<BangGiaCuoc> GetAllBangGiaCuoc(int NhaXeId)
        {
            var query = _banggiacuocRepository.Table;
            query = query.Where(m => m.NhaXeId == NhaXeId);
            return query.ToList();
        }

        public virtual void Insert(BangGiaCuoc _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _banggiacuocRepository.Insert(_item);
        }
        public virtual void Update(BangGiaCuoc _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _banggiacuocRepository.Update(_item);
        }
        public virtual void Delete(BangGiaCuoc _item)
        {
            if (_item == null)
                throw new ArgumentNullException("HangHoa");
            _banggiacuocRepository.Delete(_item);
        }
        public virtual BangGiaCuoc GetBangGiaCuocById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("HangHoa");
            return _banggiacuocRepository.GetById(id);
        }
        #endregion
    }
}
