using Nop.Core;
using Nop.Core.Domain.Chonves;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.NhaXes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.NhaXes
{
    public partial interface IKeToanService
    {
        List<ThuChi> GetAllThuChi(int NhaXeId, int LoaiThuChiId, bool? isChi, DateTime? tuNgay, DateTime? denNgay, string KeySearch);

        void Insert(ThuChi _item);
        void Update(ThuChi _item);
        void Delete(ThuChi _item);
        ThuChi GetThuChiById(int id);
        ThuChi GetThuChiByCode(string Ma);
        decimal GetTonDauKy(int NhaXeId, DateTime denNgay);
        void DeleteThuChiChuyenDi(int ChuyenDiId);
        //loai thu chi
        List<LoaiThuChi> GetAllLoaiThuChi(int NhaXeId);

        void Insert(LoaiThuChi _item);
        void Update(LoaiThuChi _item);
        void Delete(LoaiThuChi _item);
        LoaiThuChi GetLoaiThuChiById(int id);
    }
}
