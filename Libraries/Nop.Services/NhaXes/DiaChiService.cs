using Nop.Core.Data;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public class DiaChiService:IDiaChiService
    {
        private readonly IRepository<DiaChi> _tableRepository;
        private readonly IRepository<QuanHuyen> _quanhuyenRepository;
        public DiaChiService(IRepository<DiaChi> tableRepository, IRepository<QuanHuyen> quanhuyenRepository)
        {
            this._tableRepository = tableRepository;
            this._quanhuyenRepository = quanhuyenRepository;
        }
        public virtual DiaChi GetById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _tableRepository.GetById(itemId);
        }
        public virtual void Insert(DiaChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaChi");
            if (_item.QuanHuyenID == 0)
                _item.QuanHuyenID = null;
            _tableRepository.Insert(_item);
        }
        public virtual void Update(DiaChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaChi");
            if (_item.QuanHuyenID == 0)
                _item.QuanHuyenID = null;
            _tableRepository.Update(_item);
        }
        public virtual void Delete(DiaChi _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaChi");

            _tableRepository.Delete(_item);
        }
        public virtual List<QuanHuyen> GetQuanHuyenByProvinceId(int ProvinceId)
        {
            var query = _quanhuyenRepository.Table;
            query = query.Where(m => m.ProvinceID == ProvinceId);
            return query.ToList();
        }
        public virtual QuanHuyen GetQuanHuyenById(int QuanHuyenId)
        {
            return _quanhuyenRepository.GetById(QuanHuyenId);
        }
        
    }
}
