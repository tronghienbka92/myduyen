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
    public class NhanVienService : INhanVienService
    {
        private readonly IRepository<NhanVien> _tableRepository;
        public NhanVienService(IRepository<NhanVien> tableRepository)
        {
            this._tableRepository = tableRepository;
        }
        public virtual PagedList<NhanVien> GetAll(int NhaXeId = 0, string tennhanvien = "",
           int pageIndex = 0,
           int pageSize = int.MaxValue)
        {
            var query = _tableRepository.Table;
            query = query.Where(m => !m.isDelete);
            if (!String.IsNullOrWhiteSpace(tennhanvien))
                query = query.Where(m => m.HoVaTen.Contains(tennhanvien));
            if (NhaXeId > 0)
                query = query.Where(m => m.NhaXeID == NhaXeId);

            query = query.OrderBy(m => m.Id);
            return new PagedList<NhanVien>(query, pageIndex, pageSize);
        }
        public virtual List<NhanVien> GetAllLaiXePhuXeByNhaXeId(int NhaXeId, string ThongTin = "")
        {
            var query = _tableRepository.Table;
            query = query.Where(m => !m.isDelete);
            query = query.Where(m => m.NhaXeID == NhaXeId && (m.KieuNhanVienID == (int)ENKieuNhanVien.LaiXe || m.KieuNhanVienID == (int)ENKieuNhanVien.PhuXe));
            if (!string.IsNullOrEmpty(ThongTin))
            {
                query = query.Where(c=>(c.HoVaTen.Contains(ThongTin) || c.DienThoai.Contains(ThongTin)));
            }
            return query.ToList();
        }

        public virtual List<NhanVien> GetAllForGiaoDichKeVe(int VanPhongId, string TenNhanVien = "", int NhaXeId = 0)
        {
            var query = _tableRepository.Table;
            query = query.Where(m => !m.isDelete && (m.KieuNhanVienID == (int)ENKieuNhanVien.LaiXe || m.KieuNhanVienID == (int)ENKieuNhanVien.PhuXe || m.KieuNhanVienID == (int)ENKieuNhanVien.LeTan));
            if (VanPhongId > 0)
                query = query.Where(m => m.VanPhongID == VanPhongId);
            if (NhaXeId > 0)
                query = query.Where(m => m.NhaXeID == NhaXeId);
            if (!string.IsNullOrEmpty(TenNhanVien))
            {
                query = query.Where(m => m.HoVaTen.Contains(TenNhanVien));
            }
            return query.ToList();
        }
        public virtual List<NhanVien> GetAllByVanPhongId(int VanPhongId, string TenNhanVien = "")
        {
            var query = _tableRepository.Table;
            query = query.Where(m => !m.isDelete);
            if (VanPhongId > 0)
                query = query.Where(m => m.VanPhongID == VanPhongId);
            if (!string.IsNullOrEmpty(TenNhanVien))
            {
                query = query.Where(m => m.HoVaTen.Contains(TenNhanVien));
            }
            return query.ToList();
        }
        public virtual NhanVien GetById(int itemId)
        {
            if (itemId == 0)
                return null;

            return _tableRepository.GetById(itemId);
        }
        public virtual NhanVien GetByCustomerId(int CustomerId)
        {
            var query = _tableRepository.Table;
            query = query.Where(m => !m.isDelete && m.CustomerID == CustomerId);
            return query.FirstOrDefault();
        }
        public virtual void Insert(NhanVien _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhanVien");
            _item.CreatedOn = DateTime.Now;
            _tableRepository.Insert(_item);
        }
        public virtual void Update(NhanVien _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhanVien");

            _tableRepository.Update(_item);
        }
        public virtual void Delete(NhanVien _item)
        {
            if (_item == null)
                throw new ArgumentNullException("NhanVien");
            _item.isDelete = true;
            _tableRepository.Update(_item);
        }

    }
}
