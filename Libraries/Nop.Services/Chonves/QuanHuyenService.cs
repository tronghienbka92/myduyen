using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Chonves
{
    public class QuanHuyenService:IQuanHuyenService
    {
        private readonly IRepository<QuanHuyen> _tableRepository;
        private readonly IRepository<DiaDiem> _diadiemRepository;
        private readonly IRepository<StateProvince> _tinhRepository;
        public QuanHuyenService(IRepository<QuanHuyen> tableRepository,
            IRepository<DiaDiem> diadiemRepository,
            IRepository<StateProvince> tinhRepository
            )
        {
            this._tableRepository = tableRepository;
            this._diadiemRepository = diadiemRepository;
            this._tinhRepository = tinhRepository;
        }

        public virtual PagedList<QuanHuyen> GetAll(int ProvinceID = 0, string tenquanhuyen = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue)
        {
            var query = _tableRepository.Table;
            if (!String.IsNullOrWhiteSpace(tenquanhuyen))
                query = query.Where(m => m.Ten.Contains(tenquanhuyen));
            if (ProvinceID > 0)
                query = query.Where(m => m.ProvinceID == ProvinceID);

            query = query.OrderBy(m => m.Id);
            return new PagedList<QuanHuyen>(query, pageIndex, pageSize);
        }

        public virtual List<QuanHuyen> GetAllByProvinceID(int ProvinceId)
        {
            var query = _tableRepository.Table;
            query = query.Where(m => m.ProvinceID == ProvinceId);
            return query.ToList();
        }

        public virtual QuanHuyen GetById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _tableRepository.GetById(itemId);
        }
        public virtual void Insert(QuanHuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("QuanHuyen");

            _tableRepository.Insert(_item);
            var tinh = _tinhRepository.GetById(_item.ProvinceID);            
            InsertOrUpdateDiaDiem(_item.Id, ENLoaiDiaDiem.QuanHuyen, _item.Ten + " - " + tinh.Name);
        }
        public virtual void Update(QuanHuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("QuanHuyen");

            _tableRepository.Update(_item);
            var tinh = _tinhRepository.GetById(_item.ProvinceID);
            InsertOrUpdateDiaDiem(_item.Id, ENLoaiDiaDiem.QuanHuyen, _item.Ten + " - " + tinh.Name);
            
        }
        public virtual void Delete(QuanHuyen _item)
        {
            if (_item == null)
                throw new ArgumentNullException("QuanHuyen");

            _tableRepository.Delete(_item);
            DeleteDiaDiem(_item.Id, ENLoaiDiaDiem.QuanHuyen);
        }
        private void InsertOrUpdateDiaDiem(int nguonid, ENLoaiDiaDiem loai, string ten)
        {
            var item = GetDiaDiem(nguonid, loai);
            item.Ten = ten;
            item.TenKhongDau = CVCommon.convertToUnSign(ten);
            if (item.Id > 0)
                _diadiemRepository.Update(item);
            else
                _diadiemRepository.Insert(item);
        }
        private DiaDiem GetDiaDiem(int nguonid, ENLoaiDiaDiem loai)
        {
            var item = _diadiemRepository.Table.Where(c => c.NguonId == nguonid && c.LoaiId == (int)loai).First();
            if (item == null)
            {
                item = new DiaDiem();
                item.NguonId = nguonid;
                item.Loai = loai;
            }
            return item;
        }
        private void DeleteDiaDiem(int nguonid, ENLoaiDiaDiem loai)
        {
            var diadiem = GetDiaDiem(nguonid, ENLoaiDiaDiem.BenXe);
            if (diadiem.Id > 0)
                _diadiemRepository.Delete(diadiem);
        }
    }
}
