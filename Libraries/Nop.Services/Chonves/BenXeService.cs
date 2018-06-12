using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Services.Chonves
{
    public class BenXeService:IBenXeService
    {
         private readonly IRepository<BenXe> _tableRepository;
         private readonly IRepository<DiaChi> _diachiRepository;
         private readonly IRepository<DiaDiem> _diadiemRepository;
         private readonly IRepository<StateProvince> _tinhRepository;
         private readonly IRepository<QuanHuyen> _quanhuyenRepository;
         public BenXeService(IRepository<BenXe> tableRepository, IRepository<DiaChi> diachiRepository,
             IRepository<DiaDiem> diadiemRepository,
             IRepository<StateProvince> tinhRepository,
             IRepository<QuanHuyen> quanhuyenRepository
             )
        {
            this._tableRepository = tableRepository;
            this._diachiRepository = diachiRepository;
            this._diadiemRepository = diadiemRepository;
            this._tinhRepository = tinhRepository;
            this._quanhuyenRepository = quanhuyenRepository;
        }
         #region Ben Xe
         public virtual PagedList<BenXe> GetAll(int ProvinceId = 0, string tenbenxe = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue)
        {
            var query = _tableRepository.Table
                .Join(_diachiRepository.Table, x => x.DiaChiId, y => y.Id, (x, y) => new { BenXe = x, DiaChi = y })
                .Where(c => !c.BenXe.isDelete);

            if (!String.IsNullOrWhiteSpace(tenbenxe))
                query = query.Where(c => c.BenXe.TenBenXe.Contains(tenbenxe));
            if (ProvinceId > 0)
            {
                query = query.Where(c => c.DiaChi.ProvinceID == ProvinceId);
            }            
            query = query.OrderBy(c => c.BenXe.Id);
            return new PagedList<BenXe>(query.Select(c=>c.BenXe), pageIndex, pageSize);
        }

      
        public virtual List<BenXe> Search(string tenbexe = "")
        {
            var query = _tableRepository.Table;
            if (!String.IsNullOrEmpty(tenbexe))
                query = query.Where(m => m.TenBenXe.Contains(tenbexe));
            return query.ToList();
        }
        public virtual List<BenXe> GetAllBenXe()
        {
            var query= _tableRepository.Table;
            return query.ToList();
        }
        public virtual BenXe GetById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _tableRepository.GetById(itemId);
        }
        public virtual void Insert(BenXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("BenXe");

            _tableRepository.Insert(_item);
            if(_item.HienThi)
            {
                var diachi = _diachiRepository.GetById(_item.DiaChiId);
                var provice = _tinhRepository.GetById(diachi.ProvinceID);
                string tentinh = "";
                if (provice != null)
                    tentinh = provice.Name;
                InsertOrUpdateDiaDiem(_item.Id, ENLoaiDiaDiem.BenXe, _item.TenBenXe + " - " + tentinh);
            }
                
        }
        public virtual void Update(BenXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("BenXe");

            _tableRepository.Update(_item);
            if(!_item.isDelete && _item.HienThi)
            {
                var diachi = _diachiRepository.GetById(_item.DiaChiId);
                InsertOrUpdateDiaDiem(_item.Id, ENLoaiDiaDiem.BenXe, _item.TenBenXe + " - " + diachi.Province.Name);
            }                
            else
            {
                var diadiem = GetDiaDiem(_item.Id, ENLoaiDiaDiem.BenXe);
                if (diadiem.Id > 0)
                    DeleteDiaDiem(diadiem);
            }
        }
        public virtual void Delete(BenXe _item)
        {
            if (_item == null)
                throw new ArgumentNullException("BenXe");
            _item.isDelete = true;
            Update(_item);
            
        }
#endregion
        #region Dia Diem
        public virtual PagedList<DiaDiem> GetAllDiaDiem(string tendiadiem = "",
          int pageIndex = 0,
          int pageSize = int.MaxValue)
        {
            var query = _diadiemRepository.Table;

            if (!String.IsNullOrWhiteSpace(tendiadiem))
            {
                var tenkhongdau = CVCommon.convertToUnSign(tendiadiem);
                query = query.Where(c => c.Ten.Contains(tendiadiem)||c.TenKhongDau.Contains(tenkhongdau));
            }
                
            query = query.OrderBy(c => c.Id);
            return new PagedList<DiaDiem>(query, pageIndex, pageSize);
        }
        /// <summary>
        /// Thuc hien dong bo lai dia diem tu cac danh muc: tinh, quan(huyen), ben xe
        /// </summary>
        public virtual void ProcessDiaDiem()
        {
            
            var tinhs = _tinhRepository.Table.Where(c=>c.Published && c.CountryId==230).ToList();
            foreach(var t in tinhs)
            {
                InsertOrUpdateDiaDiem(t.Id, ENLoaiDiaDiem.Tinh, t.Name);               
            }
            var quanhuyens = _quanhuyenRepository.Table.ToList();
            foreach (var t in quanhuyens)
            {
                var tinh = _tinhRepository.GetById(t.ProvinceID);
                InsertOrUpdateDiaDiem(t.Id, ENLoaiDiaDiem.QuanHuyen, t.Ten+" - "+tinh.Name);
            }
            
            var benxes = _tableRepository.Table.Where(c=>c.HienThi && !c.isDelete).ToList();
            foreach (var t in benxes)
            {
                var diachi = _diachiRepository.GetById(t.DiaChiId);
                InsertOrUpdateDiaDiem(t.Id, ENLoaiDiaDiem.BenXe, t.TenBenXe + " - " + diachi.Province.Name);
            }

        }
        private void InsertOrUpdateDiaDiem(int nguonid, ENLoaiDiaDiem loai,string ten)
        {
            var item = GetDiaDiem(nguonid, loai);
            if (loai == ENLoaiDiaDiem.BenXe)
                ten = "Bến xe " + ten;
            item.Ten = ten;
            if (item.Id > 0)
                UpdateDiaDiem(item);
            else
                InsertDiaDiem(item);
        }
        private DiaDiem GetDiaDiem(int nguonid, ENLoaiDiaDiem loai)
        {
            var item= _diadiemRepository.Table.Where(c => c.NguonId == nguonid && c.LoaiId == (int)loai);
            
            if(item.Count()==0)
            {
                var dd = new DiaDiem();
                dd.NguonId = nguonid;
                dd.Loai = loai;
                return dd;
            }
            return item.First();
        }
        public virtual DiaDiem GetDiaDiemById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _diadiemRepository.GetById(itemId);
        }
        public virtual void InsertDiaDiem(DiaDiem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaDiem");            
            _item.TenKhongDau = CVCommon.convertToUnSign(_item.Ten);
            _diadiemRepository.Insert(_item);
        }
        public virtual void UpdateDiaDiem(DiaDiem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaDiem");
            _item.TenKhongDau = CVCommon.convertToUnSign(_item.Ten);
            _diadiemRepository.Update(_item);
        }
        public virtual void DeleteDiaDiem(DiaDiem _item)
        {
            if (_item == null)
                throw new ArgumentNullException("DiaDiem");
            _diadiemRepository.Delete(_item);
        }
        #endregion
    }
}
