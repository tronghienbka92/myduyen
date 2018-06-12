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
    public class GiaoDichKeVeXeService : IGiaoDichKeVeXeService
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

        private readonly IRepository<ChuyenDiTaiChinh> _chuyenditaichinhRepository;
        private readonly IRepository<ChuyenDiTaiChinhThuChi> _chuyenditaichinhthuchiRepository;

        public GiaoDichKeVeXeService(
            IRepository<ChuyenDiTaiChinh> chuyenditaichinhRepository,
            IRepository<ChuyenDiTaiChinhThuChi> chuyenditaichinhthuchiRepository,
            IRepository<XeXuatBenVeXeItem> xexuatbenvexeitemRepository,
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
            this._chuyenditaichinhRepository = chuyenditaichinhRepository;
            this._chuyenditaichinhthuchiRepository = chuyenditaichinhthuchiRepository;
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

        public virtual GiaoDichKeVe Insert(GiaoDichKeVe item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");
            _giaodichkeveRepository.Insert(item);
            item = _giaodichkeveRepository.GetById(item.Id);
            if (item.PhanLoai == ENGiaoDichKeVePhanLoai.KE_VE)
                item.Ma = string.Format("KV{0}", item.Id.ToString().PadLeft(7, '0'));
            else
                item.Ma = string.Format("TV{0}", item.Id.ToString().PadLeft(7, '0'));
            _giaodichkeveRepository.Update(item);
            return item;
        }
        public virtual void Update(GiaoDichKeVe item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");
            _giaodichkeveRepository.Update(item);
        }
        public virtual void UpdateVeXeItem(VeXeItem item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");

            _vexeitemRepository.Update(item);
        }
        public virtual bool InsertGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");

            _giaodichkevemenhgiaRepository.Insert(item);
            return true;
        }

        public virtual bool UpdateGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");
            _giaodichkevemenhgiaRepository.Update(item);
            return true;
        }

        public virtual bool DeleteGiaoDichKeVeMenhGia(GiaoDichKeVeMenhGia item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVe");

            _giaodichkevemenhgiaRepository.Delete(item);
            return true;
        }
        public virtual GiaoDichKeVe GetGiaoDichKeVeById(int itemId)
        {
            if (itemId == 0)
                return null;
            return _giaodichkeveRepository.GetById(itemId);
        }

        public virtual GiaoDichKeVeMenhGia GetGiaoDichKeVeMenhGiaById(int Id)
        {
            if (Id == 0)
                return null;
            return _giaodichkevemenhgiaRepository.GetById(Id);
        }

        public virtual List<GiaoDichKeVeMenhGia> GetAllGiaoDichMenhGiaByGiaoDichKeVeId(int giaodichkeveID)
        {
            if (giaodichkeveID == 0)
                return null;
            var query = _giaodichkevemenhgiaRepository.Table.Where(c => c.GiaoDichKeVeId == giaodichkeveID).ToList();
            return query;
        }
        public virtual List<MenhGiaVe> GetAllMenhGia(int NhaXeID)
        {
            if (NhaXeID == 0)
                return new List<MenhGiaVe>();
            var query = _menhgiaveRepository.Table.Where(c => c.NhaXeId == NhaXeID).OrderBy(c=>c.MenhGia);
            return query.ToList();

        }
        public virtual MenhGiaVe GetMenhGiaVeByGia(decimal MenhGia)
        {
            if (MenhGia == 0)
                return null;
            var query = _menhgiaveRepository.Table.Where(c => c.MenhGia == MenhGia).ToList();
            if (query.Count() > 0)
            {
                return query.First();
            }
            return null;

        }
        public virtual MenhGiaVe GetMenhGiaVeById(int MenhGiaId)
        {
            if (MenhGiaId == 0)
                return null;

            return _menhgiaveRepository.GetById(MenhGiaId);

        }
        public virtual void InsertMenhGiaVe(MenhGiaVe item)
        {
            _menhgiaveRepository.Insert(item);
        }
        public virtual void UpdateMenhGiaVe(MenhGiaVe item)
        {
            _menhgiaveRepository.Update(item);
        }
        public virtual void DeleteMenhGiaVe(MenhGiaVe item)
        {
            _menhgiaveRepository.Delete(item);
        }
        public virtual List<GiaoDichKeVe> GetAllGiaoDichKeVe(int NhaXeID, int PhanLoaiId, string MaGiaoDich, int NhanVienGiaoId, int NhanVienNhanId, DateTime? dtfrom, DateTime? dtto, ENGiaoDichKeVeTrangThai TrangThai = ENGiaoDichKeVeTrangThai.ALL, int NumTop = 100)
        {
            var query = _giaodichkeveRepository.Table.Where(c => c.NhaXeId == NhaXeID);
            if (PhanLoaiId >= 0)
            {
                query = query.Where(c => c.PhanLoaiId == PhanLoaiId);
            }
            if (TrangThai == ENGiaoDichKeVeTrangThai.ALL)
            {
                query = query.Where(c => (c.TrangThaiId == (int)ENGiaoDichKeVeTrangThai.DANG_CHINH_SUA || c.TrangThaiId == (int)ENGiaoDichKeVeTrangThai.HOAN_THANH));
            }
            else
                query = query.Where(c => c.TrangThaiId == (int)TrangThai);
            if (!String.IsNullOrEmpty(MaGiaoDich))
                query = query.Where(c => c.Ma.Contains(MaGiaoDich));
            if (NhanVienGiaoId > 0)
                query = query.Where(c => c.NguoiGiaoId == NhanVienGiaoId);
            if (NhanVienNhanId > 0)
                query = query.Where(c => c.NguoiNhanId == NhanVienNhanId);
            if (dtfrom != null)
            {
                query = query.Where(c => c.NgayKe >= dtfrom);
            }
            if (dtto != null)
            {
                dtto = dtto.GetValueOrDefault(DateTime.Now).AddDays(1);
                query = query.Where(c => c.NgayKe < dtto);
            }
            return query.OrderByDescending(c => c.Id).Take(NumTop).ToList();
        }

        public virtual List<GiaoDichKeVeMenhGia> GetTonGiaoDichKeVeMenhGia(int NhanVienId, int MenhGiaVeId, bool isVeDi, int VanPhongId, int LoaiVeId)
        {
            var items = new List<GiaoDichKeVeMenhGia>();
            var query = _vexeitemRepository.Table.Where(c => c.MenhGiaId == MenhGiaVeId && !c.isDeleted
                && c.isVeDi == isVeDi && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG);

            if (VanPhongId > 0)
                query = query.Where(c => c.VanPhongId == VanPhongId);
            else
                if (NhanVienId > 0)
                    query = query.Where(c => c.NhanVienId == NhanVienId);
            if (LoaiVeId > 0)
                query = query.Where(c => c.LoaiVeId == LoaiVeId);

            query = query.OrderBy(t => t.MauVeKyHieuId).ThenBy(m => m.ThuTuBan).ThenBy(c => c.SoSeriNum);
            var arritem = query.ToList();
            if (arritem.Count > 0)
            {
                var item = new GiaoDichKeVeMenhGia();
                item.isVeDi = isVeDi;
                item.MenhGiaId = MenhGiaVeId;
                item.SoLuong = 1;
                item.SeriFrom = arritem[0].SoSeri;
                item.ActionType = ENGiaoDichKeVeMenhGiaAction.MOI;
                item.isVeMoi = false;
                item.NguoiNhanId = NhanVienId;
                item.QuanLyMauVeKyHieuId = arritem[0].MauVeKyHieuId;
                item.quanly = GetMauVeById(item.QuanLyMauVeKyHieuId.Value);
                item.VanPhongId = arritem[0].VanPhongId;
                item.LoaiVeId = arritem[0].LoaiVeId.Value;
                int seriTo = item.SeriNumFrom;
                for (int i = 1; i < arritem.Count; i++)
                {
                    var vexe = arritem[i];
                    //kiem tra so seri tiep theo co phai la so lien tiep, va so seri cuoi cung ko chia het 100 
                    if (vexe.SoSeriNum == seriTo + 1 && seriTo % 100 != 0)
                    {
                        seriTo = vexe.SoSeriNum;
                        item.SoLuong++;
                        continue;
                    }
                    else
                    {
                        //nguoc lai ket thuc mot record menh gia, va tao moi mot record menh gia khac
                        items.Add(item);
                        //tao moi
                        item = new GiaoDichKeVeMenhGia();
                        item.isVeDi = isVeDi;
                        item.MenhGiaId = MenhGiaVeId;
                        item.SoLuong = 1;
                        item.SeriFrom = vexe.SoSeri;
                        item.ActionType = ENGiaoDichKeVeMenhGiaAction.MOI;
                        item.isVeMoi = false;
                        item.NguoiNhanId = NhanVienId;
                        item.QuanLyMauVeKyHieuId = vexe.MauVeKyHieuId;
                        item.quanly = GetMauVeById(item.QuanLyMauVeKyHieuId.Value);
                        item.VanPhongId = vexe.VanPhongId;
                        item.LoaiVeId = vexe.LoaiVeId.Value;
                        seriTo = item.SeriNumFrom;
                    }
                }
                //add record cuoi
                items.Add(item);
            }
            return items;
        }
        public virtual bool BanGiaoVe(int NhanVienId, int VanPhongId, int NguoiNhanId)
        {
            if (NhanVienId == 0)
                return false;
            if (VanPhongId == 0)
                return false;
            if (NguoiNhanId == 0)
                return false;
            var items = new List<GiaoDichKeVeMenhGia>();
            var query = _vexeitemRepository.Table.Where(c => c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG && !c.isDeleted
                && c.VanPhongId == VanPhongId
                && c.NhanVienId == NhanVienId
                && c.LoaiVeId == (int)ENLoaiVeXeItem.VeQuay
                );

            foreach (var m in query)
            {
                m.NhanVienId = NguoiNhanId;
                _vexeitemRepository.Update(m);
            }
            return true;
        }

        #region nghiep vu nhap ve xe
        public virtual bool isExistVeXeItem(int NhaXeId, string SoSeri, string MauVe, string KyHieu)
        {
            if (_vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.MauVe == MauVe && c.KyHieu == KyHieu && c.SoSeri == SoSeri).Any())
                return true;
            return false;
        }
        /// <summary>
        /// kiem tra so seri day co ton tai va chua duoc ban 
        /// </summary>
        /// <param name="SoSeri"></param>
        /// <returns></returns>
        public virtual bool isExistVeXeItem(string SoSeriFrom, string SoSeriTo)
        {
            int _SoSeriFrom = Convert.ToInt32(SoSeriFrom);
            int _SoSeriTo = Convert.ToInt32(SoSeriTo);
            var query = _vexeitemRepository.Table.Where(c => !c.isDeleted && c.NgayBan == null && c.SoSeriNum >= _SoSeriFrom && c.SoSeriNum <= _SoSeriTo).Any();
            return query;


        }
        public virtual bool isExistVeXeItem(int NhaXeId, string MauVe, string KyHieu, string SoSeriFrom, int SoLuong, bool isKeVe = true)
        {
            int _SoSeriFrom = Convert.ToInt32(SoSeriFrom);
            int _SoSeriTo = _SoSeriFrom + SoLuong - 1;
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.MauVe == MauVe && c.KyHieu == KyHieu && c.SoSeriNum >= _SoSeriFrom && c.SoSeriNum <= _SoSeriTo);
            //neu dang o viec kiem tra giao dich ke ve, thi nhung ve o trang thai moi thi bo qua
            if (isKeVe)
                query = query.Where(c => c.TrangThaiId != (int)ENVeXeItemTrangThai.MOI_TAO);
            return query.Any();
        }
        public virtual VeXeItem GetVeXeItem(int NhaXeId, string SoSeri, string MauVe, string KyHieu)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.MauVe == MauVe && c.KyHieu == KyHieu && c.SoSeri == SoSeri).FirstOrDefault();
            return query;
        }
        public virtual VeXeItem GetVeXeItemById(int Id)
        {
            if (Id == 0)
                return null;
            return _vexeitemRepository.GetById(Id);
        }

        public virtual List<VeXeItem> GetVeXeItems(int nhaxeID, int NhanVienId = 0, ENVeXeItemTrangThai TrangThaiId = ENVeXeItemTrangThai.ALL, int MenhGiaId = 0, int VanPhongId = 0, int MauVeId = 0, string ThongTin = "", int NumRow = 500)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted);
            if (NhanVienId > 0)
                query = query.Where(c => c.NhanVienId == NhanVienId);
            if (TrangThaiId != ENVeXeItemTrangThai.ALL)
                query = query.Where(c => c.TrangThaiId == (int)TrangThaiId);
            if (MenhGiaId > 0)
                query = query.Where(c => c.MenhGiaId == MenhGiaId);
            if (VanPhongId > 0)
                query = query.Where(c => c.VanPhongId == VanPhongId);
            if (MauVeId > 0)
                query = query.Where(c => c.MauVeKyHieuId == MauVeId);
            if (!string.IsNullOrEmpty(ThongTin))
            {
                //ThongTin=Seri vé: 0071600 hoặc 0071600-0071699
                int soserifrom = 0;
                int soserito = 0;
                try
                {
                    string[] arrseri = ThongTin.Split('-');
                    if (arrseri.Length == 1 && int.TryParse(arrseri[0], out soserifrom))
                    {                        
                        soserito = soserifrom;
                    }
                    else
                    {
                        int.TryParse(arrseri[0], out soserifrom);
                        int.TryParse(arrseri[1], out soserito);
                    }
                    query = query.Where(c => c.SoSeriNum >= soserifrom && c.SoSeriNum <= soserito);
                }
                catch
                {  }
            }            
            if (NumRow > 0)
                return query.Take(NumRow).ToList();
            return query.ToList();
        }
        /// <summary>
        /// Cap nhat nhung ve thanh trang thai chua ban
        /// Chuc nang nay chi phuc vu trong truong hop cap nhap thong tin nham
        /// Chi nhung ve cua lai xe va tiep vien moi dc cap nhat
        /// </summary>
        /// <param name="arrVeId"></param>
        public virtual void UpdateVeSangDaBan(int[] arrVeId)
        {
            var vexes = _vexeitemRepository.Table.Where(c => !c.isDeleted && c.VanPhongId > 0 && arrVeId.Contains(c.Id) && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_SU_DUNG).ToList();
            foreach (var item in vexes)
            {
                item.TrangThai = ENVeXeItemTrangThai.DA_BAN;
                item.NguonVeXeId = null;
                item.XeXuatBenId = null;
                _vexeitemRepository.Update(item);
            }

        }
        /// <summary>
        /// Cap nhat nhung ve thanh trang thai chua ban
        /// Chuc nang nay chi phuc vu trong truong hop cap nhap thong tin nham
        /// Chi nhung ve cua lai xe va tiep vien moi dc cap nhat
        /// </summary>
        /// <param name="arrVeId"></param>
        public virtual void UpdateVeSangChuaBan(int[] arrVeId)
        {
            var vexes = _vexeitemRepository.Table.Where(c => !c.isDeleted && !(c.VanPhongId>0)  && arrVeId.Contains(c.Id) && (c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN || c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_SU_DUNG)).ToList();
            foreach(var item in vexes)
            {
                item.TrangThai = ENVeXeItemTrangThai.DA_GIAO_HANG;
                item.NgayBan = null;
                item.NgayDi = null;
                item.NguonVeXeId = null;
                item.XeXuatBenId = null;                
                _vexeitemRepository.Update(item);
            }
            
        }
        /// <summary>
        /// Cap nhat nhung ve thanh trang thai xoa
        /// Chuc nang nay chi phuc vu trong truong hop ke ve nham
        /// Chi xoa dc nhung ve dang o trang thai moi
        /// </summary>
        /// <param name="arrVeId"></param>
        public virtual void DeleteVeXe(int[] arrVeId)
        {
            var vexes = _vexeitemRepository.Table.Where(c => !c.isDeleted && arrVeId.Contains(c.Id) && c.TrangThaiId == (int)ENVeXeItemTrangThai.MOI_TAO).ToList();
            foreach (var item in vexes)
            {
                item.isDeleted = true;
                _vexeitemRepository.Update(item);
            }
        }
        public virtual List<VeXeItem> GetVeXeItems(int XeXuatBenId)
        {
            var query = _vexeitemRepository.Table.Where(c => c.XeXuatBenId == XeXuatBenId && !c.isDeleted);
            return query.ToList();
        }
        public virtual List<VeXeItem> GetAllVeXeLuotDi(int nhaxeID)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN && c.isVeDi == true && !c.isDeleted).ToList();
            return query;
        }
        public virtual List<VeXeItem> GetAllVeXeLuotVe(int nhaxeID)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN && c.isVeDi == false && !c.isDeleted).ToList();
            return query;
        }
        public virtual List<VeXeItem> GetVeXeBanByDay(int nhaxeID, DateTime NgayBan, int HanhTrinhId = 0, int NhanVienId = 0, ENVeXeItemTrangThai TrangThaiId = ENVeXeItemTrangThai.ALL)
        {


            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted
                && c.NgayBan != null
                && c.NgayBan.Value.Day == NgayBan.Day
                && c.NgayBan.Value.Month == NgayBan.Month
                && c.NgayBan.Value.Year == NgayBan.Year
                );
            //lay thong tin tu hanh trinh
            if (HanhTrinhId > 0)
            {
                var arrchangids = _hanhtrinhgiaveRepository.Table.Where(c => c.HanhTrinhId == HanhTrinhId).Select(d => d.Id).ToList();
                query = query.Where(c => c.ChangId != null && arrchangids.Contains(c.ChangId.GetValueOrDefault(0)));
            }
            if (NhanVienId > 0)
                query = query.Where(c => c.NhanVienId == NhanVienId);
            if (TrangThaiId != ENVeXeItemTrangThai.ALL)
                query = query.Where(c => c.TrangThaiId == (int)TrangThaiId);

            return query.ToList();
        }
        /// <summary>
        /// Lay thong tin ve xe ban tai quay truc luc xe chay, lay de cap nhat vao chuyen di de tinh doanh thu tung xe
        /// Nhung ve dc lay o day la :
        /// Ve ban truoc luc xe chay
        /// Ve chua dc gan nguonvexeid
        /// Ve ban trong ngay
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="chuyenxe"></param>
        /// <returns></returns>
        public virtual List<VeXeItem> GetVeXeBanTaiQuay(int NhaXeId, HistoryXeXuatBen chuyenxe, bool isSoHuu = false)
        {
            //lay thong tin ngay, gio di
            var ngaydi = chuyenxe.NgayDi;
            var ngaydimin = ngaydi.AddHours(1);
            //lay thong tin cac chang trong hanh trinh
            var changids = _hanhtrinhgiaveRepository.Table.Where(c => c.HanhTrinhId == chuyenxe.HanhTrinhId).Select(m => m.Id).ToList();
            //lay thong tin ve ban o quay
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.VanPhongId > 0);
            if (!isSoHuu)
            {
                query = query.Where(c => c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN
                && c.ChangId > 0
                && changids.Contains(c.ChangId.Value)
                && c.NgayBan <= ngaydi);
            }
            else
            {
                query = query.Where(c => c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_SU_DUNG
                    && c.XeXuatBenId == chuyenxe.Id
                    && c.ChangId > 0
                    && changids.Contains(c.ChangId.Value)
                    );
            }

            return query.OrderByDescending(t => t.NgayBan).ToList();

        }
        public virtual List<VeXeItem> GetVeXeItemsQuay(int XeXuatBenId)
        {
            var query = _vexeitemRepository.Table.Where(c => c.XeXuatBenId == XeXuatBenId && !c.isDeleted
                && c.LoaiVeId != (int)ENLoaiVeXeItem.VeThuong);

            return query.ToList();
        }
        /// <summary>
        /// Lay thong tin ve da ban tren xe
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="chuyenxe"></param>
        /// <returns></returns>
        public virtual List<VeXeItem> GetVeXeBanTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe)
        {
            //lay thong tin cac chang trong hanh trinh
            var changids = _hanhtrinhgiaveRepository.Table.Where(c => c.HanhTrinhId == chuyenxe.HanhTrinhId).Select(m => m.Id).ToList();

            //lay thong tin ve ban tren xe
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted
                && !(c.VanPhongId > 0)
                && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_SU_DUNG
                && c.XeXuatBenId == chuyenxe.Id
                );
            //khong sung dieu kien ve cua nhan vien o day, co the ban ve cua nhan vien khac
            //&& arrnhanvienxe.Contains(c.NhanVienId.Value)
            return query.ToList();
        }
        /// <summary>
        /// Lay thong tin ve da ban tren xe
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="chuyenxe"></param>
        /// <returns></returns>
        public virtual List<VeXeItem> GetVeXeDuKienBanTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, int SoLuong, List<VeXeItem> arrVeDaCo)
        {
            //lay thong tin menh gia
            var htgiave = _hanhtrinhgiaveRepository.GetById(ChangId);
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == htgiave.GiaVe).FirstOrDefault();
            if (menhgia == null)
                throw new ArgumentNullException("GetVeXeDuKienBanTrenXe");

            var arrnhanvienxe = chuyenxe.LaiPhuXes.Select(c => c.NhanVien_Id).ToArray();
            var veids = arrVeDaCo.Select(c => c.Id).ToArray();
            //lay thong tin ve ban tren xe
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted
                && !(c.VanPhongId > 0)
                && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG
                && c.isVeDi == chuyenxe.HanhTrinh.isTuyenDi
                && c.MenhGiaId == menhgia.Id
                && arrnhanvienxe.Contains(c.NhanVienId.Value)
                && !veids.Contains(c.Id)
                );

            return query.Take(SoLuong).ToList();
        }
        public virtual List<VeXeItem> KiemTraBanVeTaiQuayTheoXe(int NhaXeId, HistoryXeXuatBen chuyenxe, string[] arrseri, out string serikohople)
        {
            serikohople = "";
            var arrret = new List<VeXeItem>();
            var mauves = _quanlymauvekyhieuRepository.Table.OrderByDescending(c => c.Id).ToList();
            var mauvedangdung = mauves.FirstOrDefault();
            //xu ly ban ve
            foreach (var v in arrseri)
            {
                string[] seriinfo = v.Split('-');

                string SeriVe = "-1";
                if (seriinfo.Length == 1)
                {
                    mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = v.Trim();
                }
                else if (seriinfo.Length == 3)
                {
                    mauvedangdung = mauves.Where(c => c.MauVe.Contains(seriinfo[0]) && c.KyHieu.Contains(seriinfo[1])).FirstOrDefault();
                    if (mauvedangdung == null)
                        mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = seriinfo[2];
                }
                int _serinum;
                int.TryParse(SeriVe, out _serinum);

                var query = _vexeitemRepository.Table.Where(c => c.SoSeriNum == _serinum && !c.isDeleted
                    && c.MauVeKyHieuId == mauvedangdung.Id
                    && c.isVeDi == chuyenxe.HanhTrinh.isTuyenDi
                    );
                var vexe = query.FirstOrDefault();
                if (vexe != null)
                {
                    //ve dang la so huu cua chuyen xe
                    if (vexe.TrangThai == ENVeXeItemTrangThai.DA_SU_DUNG && vexe.XeXuatBenId.Value == chuyenxe.Id)
                    {
                        //do nothing
                        //ve van hop le
                    }
                    else if (vexe.TrangThai != ENVeXeItemTrangThai.DA_BAN)
                    {
                        //neu ve khong o trang thai da ban thi khong hop le
                        serikohople = serikohople + v + ",";
                        continue;
                    }
                }
                else
                {
                    serikohople = serikohople + v + ",";
                    continue;
                }
                arrret.Add(vexe);

            }
            return arrret;
        }
        public virtual List<VeXeItem> KiemTraBanVeTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, string[] arrseri, out string serikohople)
        {
            serikohople = "";
            var arrret = new List<VeXeItem>();
            var mauves = _quanlymauvekyhieuRepository.Table.OrderByDescending(c => c.Id).ToList();
            var mauvedangdung = mauves.FirstOrDefault();
            //xu ly ban ve
            foreach (var v in arrseri)
            {
                string[] seriinfo = v.Split('-');
                string SeriVe = "-1";
                if (seriinfo.Length == 1)
                {
                    mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = v.Trim();
                }
                else if (seriinfo.Length == 3)
                {

                    mauvedangdung = mauves.Where(c => c.MauVe.Contains(seriinfo[0]) && c.KyHieu.Contains(seriinfo[1])).FirstOrDefault();
                    if (mauvedangdung == null)
                        mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = seriinfo[2];
                }
                int _serinum = Convert.ToInt32(SeriVe);

                var query = _vexeitemRepository.Table.Where(c => c.SoSeriNum == _serinum && !c.isDeleted
                    && c.MauVeKyHieuId == mauvedangdung.Id
                    && c.isVeDi == chuyenxe.HanhTrinh.isTuyenDi
                    );
                var vexe = query.FirstOrDefault();
                if (vexe != null)
                {
                    //ve dang la so huu cua chuyen xe
                    if (vexe.TrangThai == ENVeXeItemTrangThai.DA_SU_DUNG && vexe.XeXuatBenId == chuyenxe.Id)
                    {
                        //do nothing
                        //ve van hop le
                    }
                    else if (vexe.TrangThai != ENVeXeItemTrangThai.DA_GIAO_HANG)
                    {
                        //neu ve khong o trang thai dang giao hang thi khong hop le, ve tren xe chua ban cho ai ca
                        serikohople = serikohople + v + ",";
                        continue;
                    }
                }
                else
                {
                    serikohople = serikohople + v + ",";
                    continue;
                }

                arrret.Add(vexe);

            }
            return arrret;
        }

        public virtual List<VeXeItem> CapNhatBanVeTaiQuayTheoXe(int NhaXeId, HistoryXeXuatBen chuyenxe, string[] arrseri)
        {
            var arrret = new List<VeXeItem>();
            var mauves = _quanlymauvekyhieuRepository.Table.OrderByDescending(c => c.Id).ToList();
            var mauvedangdung = mauves.FirstOrDefault();
            var ngaydi = chuyenxe.NgayDi.AddHours(chuyenxe.NguonVeInfo.ThoiGianDi.Hour).AddMinutes(chuyenxe.NguonVeInfo.ThoiGianDi.Minute);
            //xu ly ban ve
            foreach (var v in arrseri)
            {
                string[] seriinfo = v.Split('-');
                string SeriVe = "-1";
                if (seriinfo.Length == 1)
                {
                    mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = v.Trim();
                }
                else if (seriinfo.Length == 3)
                {

                    mauvedangdung = mauves.Where(c => c.MauVe.Contains(seriinfo[0]) && c.KyHieu.Contains(seriinfo[1])).FirstOrDefault();
                    if (mauvedangdung == null)
                        mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = seriinfo[2];
                }
                int _serinum = Convert.ToInt32(SeriVe);


                var query = _vexeitemRepository.Table.Where(c => c.SoSeriNum == _serinum && !c.isDeleted
                    && c.MauVeKyHieuId == mauvedangdung.Id
                    && c.isVeDi == chuyenxe.HanhTrinh.isTuyenDi
                    );
                var vexe = query.FirstOrDefault();
                if (vexe == null)
                    continue;
                if (vexe.TrangThai == ENVeXeItemTrangThai.DA_SU_DUNG && vexe.XeXuatBenId == chuyenxe.Id)
                {
                    //do nothing
                    //ve van hop le
                    arrret.Add(vexe);
                    continue;
                }
                else if (vexe.TrangThai != ENVeXeItemTrangThai.DA_BAN)
                {
                    //neu ve khong o trang thai dang giao hang thi khong hop le, ve tren xe chua ban cho ai ca                    
                    continue;
                }
                vexe.NgayDi = ngaydi;
                vexe.NguonVeXeId = chuyenxe.NguonVeId;
                vexe.TrangThai = ENVeXeItemTrangThai.DA_SU_DUNG;
                vexe.XeXuatBenId = chuyenxe.Id;
                UpdateVeXeItem(vexe);
                //cap nhat phoi ve
                var queryphoive = _phoiveRepository.Table.Where(c => c.VeXeItemId == vexe.Id);
                var phoive = queryphoive.FirstOrDefault();
                if (phoive != null)
                {
                    phoive.NguonVeXeId = chuyenxe.NguonVeId;
                    phoive.ChangId = vexe.ChangId.Value;
                    _phoiveRepository.Update(phoive);
                }
                arrret.Add(vexe);

            }
            return arrret;
        }
        public virtual List<VeXeItem> CapNhatBanVeTrenXe(int NhaXeId, HistoryXeXuatBen chuyenxe, int ChangId, string[] arrseri)
        {
            var arrret = new List<VeXeItem>();
            var mauves = _quanlymauvekyhieuRepository.Table.OrderByDescending(c => c.Id).ToList();
            var mauvedangdung = mauves.FirstOrDefault();
            var ngaydi = chuyenxe.NgayDi.AddHours(chuyenxe.NguonVeInfo.ThoiGianDi.Hour).AddMinutes(chuyenxe.NguonVeInfo.ThoiGianDi.Minute);
            //xu ly ban ve
            foreach (var v in arrseri)
            {
                string[] seriinfo = v.Split('-');
                string SeriVe = "-1";
                if (seriinfo.Length == 1)
                {
                    mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = v.Trim();
                }
                else if (seriinfo.Length == 3)
                {
                    mauvedangdung = mauves.Where(c => c.MauVe.Contains(seriinfo[0]) && c.KyHieu.Contains(seriinfo[1])).FirstOrDefault();
                    if (mauvedangdung == null)
                        mauvedangdung = mauves.FirstOrDefault();
                    SeriVe = seriinfo[2];
                }
                int _serinum = Convert.ToInt32(SeriVe);

                var query = _vexeitemRepository.Table.Where(c => c.SoSeriNum == _serinum && !c.isDeleted
                   && c.MauVeKyHieuId == mauvedangdung.Id
                   && c.isVeDi == chuyenxe.HanhTrinh.isTuyenDi
                   );
                var vexe = query.FirstOrDefault();
                if (vexe == null)
                    continue;
                if (vexe.TrangThai == ENVeXeItemTrangThai.DA_SU_DUNG && vexe.XeXuatBenId == chuyenxe.Id)
                {
                    //do nothing
                    //ve van hop le
                    arrret.Add(vexe);
                    continue;
                }
                else if (vexe.TrangThai != ENVeXeItemTrangThai.DA_GIAO_HANG)
                {
                    //neu ve khong o trang thai dang giao hang thi khong hop le, ve tren xe chua ban cho ai ca                    
                    continue;
                }
                vexe.ChangId = ChangId;
                vexe.NgayBan = ngaydi;
                vexe.NgayDi = ngaydi;
                vexe.NguonVeXeId = chuyenxe.NguonVeId;
                vexe.TrangThai = ENVeXeItemTrangThai.DA_SU_DUNG;
                vexe.XeXuatBenId = chuyenxe.Id;
                UpdateVeXeItem(vexe);

                //tao thong tin phoi ve
                var phoive = new PhoiVe();
                phoive.NguonVeXeId = chuyenxe.NguonVeId;
                phoive.NgayTao = DateTime.Now;
                phoive.NgayUpd = DateTime.Now;
                phoive.NgayDi = chuyenxe.NgayDi;
                phoive.TrangThai = ENTrangThaiPhoiVe.KetThuc;

                phoive.GiaVeHienTai = vexe.GiaVe;
                phoive.ChangId = ChangId;
                phoive.NguoiDatVeId = vexe.NhanVienId;
                phoive.CustomerId = CommonHelper.KhachVangLaiId;//khach vang lai
                phoive.MaVe = vexe.SoSeri;
                phoive.VeXeItemId = vexe.Id;
                _phoiveRepository.Insert(phoive);

                arrret.Add(vexe);

            }
            return arrret;
        }
        public virtual List<VeXeItem> GetAllMenhGia(int nguonveId, int NhaXeId)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NguonVeXeId == nguonveId && c.NhaXeId == NhaXeId && !c.isDeleted && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG).ToList();
            return query;
        }
        public virtual List<VeXeItem> GetVeXeBanQuayByDay(int nhaxeID, DateTime NgayBan, int ChangId, int HanhTrinhId = 0, int NhanVienId = 0, Decimal giave = 0, bool isVeDi = true)
        {


            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted
                && c.NgayBan != null
                && c.NgayBan.Value.Day == NgayBan.Day
                && c.NgayBan.Value.Month == NgayBan.Month
                && c.NgayBan.Value.Year == NgayBan.Year
                );
            //lay thong tin tu hanh trinh
            if (HanhTrinhId > 0)
            {
                var arrchangids = _hanhtrinhgiaveRepository.Table.Where(c => c.HanhTrinhId == HanhTrinhId).Select(d => d.Id).ToList();
                query = query.Where(c => c.ChangId != null && arrchangids.Contains(c.ChangId.Value));
            }
            if (NhanVienId > 0)
            {
                query = query.Where(c => c.NhanVienId == NhanVienId
                    && (c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN || c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_SU_DUNG)
                   && c.isVeDi == isVeDi
                   && c.ChangId == ChangId).OrderByDescending(c => c.Id);
            }


            return query.ToList();
        }
        public virtual bool InsertVeXeItem(VeXeItem itemVeXe)
        {
            if (itemVeXe == null)
                throw new ArgumentNullException("VeXeItem");

            var query = _vexeitemRepository.Table.Where(c => c.SoSeri == itemVeXe.SoSeri && !c.isDeleted && c.NhaXeId == itemVeXe.NhaXeId);
            if (isExistVeXeItem(itemVeXe.NhaXeId, itemVeXe.SoSeri, itemVeXe.MauVe, itemVeXe.KyHieu))
                return false;
            _vexeitemRepository.Insert(itemVeXe);
            return true;
        }
        public virtual bool InsertVeXeItem(IEnumerable<VeXeItem> vexes)
        {
            if (vexes == null)
                throw new ArgumentNullException("vexes");
            _vexeitemRepository.Insert(vexes);
            return true;
        }
        public virtual void EmptyGiaoDichKeVeXeItem(int Id)
        {
            var vexeitemtgs = _giaodichkevexeitemRepository.Table.Where(c => c.GiaoDichKeVeId == Id).ToList();
            //var vexeitemids=vexeitemtgs.Where(c=>c.kevemenhgia.isVeMoi).Select(d=>d.VeXeItemId).ToList();
            //var vexeitems = _vexeitemRepository.Table.Where(c => vexeitemids.Contains(c.Id)).ToList();
            _giaodichkevexeitemRepository.Delete(vexeitemtgs);
            //_vexeitemRepository.Delete(vexeitems);
        }
        #endregion

        /// insert thong tin vao bang giao dich ke ve xe item
        /// 
        public virtual bool InsertGiaoDichKeVeXeItem(GiaoDichKeVeXeItem item)
        {
            if (item == null)
                throw new ArgumentNullException("GiaoDichKeVeXeItem");
            _giaodichkevexeitemRepository.Insert(item);
            return true;
        }
        public virtual bool InsertGiaoDichKeVeXeItem(IEnumerable<GiaoDichKeVeXeItem> items)
        {
            _giaodichkevexeitemRepository.Insert(items);
            return true;
        }

        // insert thong tin quan ly mau ve va ky hieu
        public virtual bool InsertQuanLyMauVeKyHieu(QuanLyMauVeKyHieu item)
        {
            if (item == null)
                throw new ArgumentNullException("QuanLyMauVeKyHieu");
            //var query = _quanlymauvekyhieuRepository.Table.Where(c => c.MauVe == item.MauVe && c.KyHieu == item.KyHieu && c.NhaXeId == item.NhaXeId);
            //if (query.Count() > 0)
            //    return false;
            _quanlymauvekyhieuRepository.Insert(item);
            return true;
        }

        public virtual bool UpdateQuanLyMauVeKyHieu(QuanLyMauVeKyHieu item)
        {
            if (item == null)
                throw new ArgumentNullException("QuanLyMauVeKyHieu");
            //var query = _quanlymauvekyhieuRepository.Table.Where(c => c.MauVe == item.MauVe && c.KyHieu == item.KyHieu && c.NhaXeId == item.NhaXeId);
            //if (query.Count() > 0)
            //    return false;
            _quanlymauvekyhieuRepository.Update(item);
            return true;
        }

        public virtual QuanLyMauVeKyHieu GetMauVeById(int Id)
        {
            if (Id == 0)
                return null;
            return _quanlymauvekyhieuRepository.GetById(Id);
        }

        public virtual List<QuanLyMauVeKyHieu> GetAllMauVeKyHieu(int NhaXeId)
        {
            if (NhaXeId == 0)
                return null;
            var query = _quanlymauvekyhieuRepository.Table.Where(c => c.NhaXeId == NhaXeId).OrderByDescending(o=>o.isMacDinh).ThenByDescending(m=>m.Id).ToList();
            return query;
        }

        public virtual List<int> GetMenhGiaId(int NhaxeId)
        {
            List<int> menhgiaIds = new List<int>();
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaxeId && !c.isDeleted && (c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG)).Select(c => c.MenhGiaId);
            if (query.Count() > 0)
                menhgiaIds = query.ToList();
            return menhgiaIds;
        }

        public virtual VeXeItem BanVe(int NhaXeId, int NhanVienId, int XeXuatBenId, int NguonVeXeId, int ChangId, Decimal giave, bool isVeDi)
        {
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == giave).FirstOrDefault();
            if (menhgia == null)
                return null;
            //lay thong tin ve dau tien
            var vexeitemfirst = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.NhanVienId == NhanVienId && c.MenhGiaId == menhgia.Id && c.isVeDi == isVeDi && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG && c.NgayBan == null).FirstOrDefault();
            if (vexeitemfirst == null)
            {
                //tao moi thong tin ve, luc dat ve tu tab
                vexeitemfirst = new VeXeItem();
                vexeitemfirst.NhaXeId = NhaXeId;
                vexeitemfirst.NhanVienId = NhanVienId;
                vexeitemfirst.MenhGiaId = menhgia.Id;
                vexeitemfirst.isVeDi = isVeDi;
                vexeitemfirst.TrangThaiId = (int)ENVeXeItemTrangThai.DA_GIAO_HANG;
                vexeitemfirst.isDeleted = false;
                vexeitemfirst.NgayTao = DateTime.Now;
                vexeitemfirst.NgayNhap = DateTime.Now;
                vexeitemfirst.NgayGiaoVe = DateTime.Now;
                vexeitemfirst.LoaiVeId = (int)ENLoaiVeXeItem.VeThuong;
                vexeitemfirst.MauVeKyHieuId = 2;
                _vexeitemRepository.Insert(vexeitemfirst);
                vexeitemfirst = _vexeitemRepository.GetById(vexeitemfirst.Id);
                vexeitemfirst.SoSeri = vexeitemfirst.Id.ToString();
                vexeitemfirst.SoSeriNum = vexeitemfirst.Id;

                
            }
                
            vexeitemfirst.NgayBan = DateTime.Now;
            vexeitemfirst.TrangThai = ENVeXeItemTrangThai.DA_BAN;
            vexeitemfirst.XeXuatBenId = XeXuatBenId;
            if (ChangId > 0)
            {
                vexeitemfirst.ChangId = ChangId;
            }
            if (NguonVeXeId > 0)
            {
                vexeitemfirst.NguonVeXeId = NguonVeXeId;
                vexeitemfirst.TrangThai = ENVeXeItemTrangThai.DA_SU_DUNG;
                vexeitemfirst.NgayDi = DateTime.Now;
            }

            _vexeitemRepository.Update(vexeitemfirst);
            return vexeitemfirst;
        }
        /// <summary>
        /// Lay thong tin ve va cap nhat ve da su dung 
        /// Su dung trong truong hop lai xe nhap so seri ve do khach mua o quay ve
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="NguonVeXeId"></param>
        /// <param name="ChangId"></param>
        /// <param name="SoSeri"></param>
        /// <param name="isVeDi"></param>
        /// <returns></returns>
        /// 
        public virtual VeXeItem SuDungVe(int NhaXeId, int NguonVeXeId, string SoSeri, bool isVeDi)
        {
            DateTime ngaydi = DateTime.Now;
            var vexeitemfirst = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.isVeDi == isVeDi && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN
                && c.SoSeri == SoSeri
                && c.NgayDi.HasValue
                && c.NgayDi.Value.Day == ngaydi.Day
                && c.NgayDi.Value.Month == ngaydi.Month
                && c.NgayDi.Value.Year == ngaydi.Year
                ).FirstOrDefault();
            if (vexeitemfirst != null)
            {
                vexeitemfirst.NguonVeXeId = NguonVeXeId;
                vexeitemfirst.TrangThai = ENVeXeItemTrangThai.DA_SU_DUNG;
                _vexeitemRepository.Update(vexeitemfirst);
            }
            return vexeitemfirst;
        }
        public virtual VeXeItem BanVeTaiQuay(int NhaXeId, int NhanVienId, int ChangId, Decimal giave, bool isVeDi, DateTime NgayDi, int VanPhongId, int QuyenId = 0)
        {
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == giave).FirstOrDefault();
            if (menhgia == null)
                return null;
            //lay thong tin ve dau tien
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.VanPhongId == VanPhongId && c.MenhGiaId == menhgia.Id && c.isVeDi == isVeDi && c.NgayBan == null);
            if (QuyenId > 0)
                query = query.Where(c => c.QuyenId == QuyenId);
            query = query.OrderBy(q => q.ThuTuBan).ThenBy(m => m.Id);
            var vexeitemfirst = query.FirstOrDefault();
            if (vexeitemfirst == null)
                return null;
            vexeitemfirst.NgayBan = DateTime.Now;
            vexeitemfirst.NgayDi = NgayDi;
            vexeitemfirst.NhanVienId = NhanVienId;
            vexeitemfirst.TrangThai = ENVeXeItemTrangThai.DA_BAN;
            if (ChangId > 0)
            {
                vexeitemfirst.ChangId = ChangId;
            }

            _vexeitemRepository.Update(vexeitemfirst);

            return vexeitemfirst;
        }
        public virtual List<ThongKeVeTiepVienDaDung> GetVeTiepVienTheoQuyen(int nhaxeID, bool IsCon, int Thang, int Nam, string ThongTin)
        {

            var Items = new List<ThongKeVeTiepVienDaDung>();

            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && c.NgayDi.Value.Year == Nam
                         && c.NgayDi.Value.Month == Thang
              && c.NhanVienId != null && c.LoaiVeId == (int)ENLoaiVeXeItem.VeThuong).Select(c => c.SoQuyen).Distinct().ToList();


            if (!string.IsNullOrEmpty(ThongTin))
            {
                //ThongTin=quyen vé: 0071600 hoặc 0071600-0071699
                int quyenfrom = 0;
                int quyento = 0;
                try
                {
                    string[] arrquyen = ThongTin.Split('-');
                    if (arrquyen.Length == 1 && int.TryParse(arrquyen[0], out quyenfrom))
                    {
                        quyento = quyenfrom;
                    }
                    else
                    {
                        int.TryParse(arrquyen[0], out quyenfrom);
                        int.TryParse(arrquyen[1], out quyento);
                    }
                    query = query.Where(c => c >= quyenfrom && c <= quyento).ToList();
                }
                catch
                { }
            }


            foreach (var item in query)
            {
                var m = new ThongKeVeTiepVienDaDung();
                m.SoQuyen = item;
                var VeInQuyen = GetVeByQuyen(item);
                var vexes = VeInQuyen.OrderBy(c => c.SoSeriNum);
                var SeriStat = vexes.First();
                m.SeriFrom = SeriStat.SoSeriNum;
                m.SeriTo = vexes.Last().SoSeriNum;
                m.MenhGia = SeriStat.menhgia.MenhGia;
                m.isCon = false;
                m.NgayNhan = SeriStat.NgayNhap;
                if (SeriStat.nhanvien != null)
                    m.TenNguoiNhan = SeriStat.nhanvien.HoVaTen;
                var VeXesCon = VeInQuyen.Where(c => c.NgayBan == null).OrderBy(c => c.SoSeriNum);
                if (VeXesCon.Count() > 0)
                {
                    m.isCon = true;
                    m.SoLuongCon = VeXesCon.Count();
                    m.SeriConFrom = VeXesCon.First().SoSeriNum;
                    m.SeriConTo = VeXesCon.Last().SoSeriNum;
                    m.SoLuongDungs = VeInQuyen.Where(c => c.NgayDi != null
                        && c.NgayDi.Value.Year == Nam
                        && c.NgayDi.Value.Month == Thang)
                        .Select(c => new { nam = c.NgayDi.Value.Year, thang = c.NgayDi.Value.Month, ngay = c.NgayDi.Value.Day, soquyen = c.SoQuyen })
                         .GroupBy(c => c.ngay)
                         .Select(c => new NgayDungItem
                         {
                             Ngay = c.Key,
                             Thang = c.First().thang,
                             Nam = c.First().nam,
                             SoQuyen = c.First().soquyen,
                             SoVe = c.Count()

                         }).ToList();
                }

                Items.Add(m);
            }
            return Items.Where(c => c.isCon == IsCon).OrderBy(c => c.SoQuyen).ToList();
        }
        List<VeXeItem> GetVeByQuyen(int SoQuyen)
        {

            var query = _vexeitemRepository.Table.Where(c => !c.isDeleted
                && c.SoQuyen == SoQuyen);

            return query.ToList();
        }
        public virtual bool TieuVe(int NhaXeId, int SeriFrom, int MenhGiaId, int NhanVienId, bool IsVeDi)
        {
            if (SeriFrom == 0)
                return false;
            var vexeitems = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.NhanVienId == NhanVienId && c.MenhGiaId == MenhGiaId && c.isVeDi == IsVeDi && c.NgayBan == null)
                .OrderBy(c => c.SoSeriNum)
                .Where(c => c.SoSeriNum < SeriFrom)
                .ToList();
            foreach (var m in vexeitems)
            {
                m.NgayBan = DateTime.Now;
                m.NgayDi = DateTime.Now;
                m.TrangThai = ENVeXeItemTrangThai.DA_BAN;
                _vexeitemRepository.Update(m);
            }
            return true;
        }
        public virtual VeXeItem GetVeChuanBiBan(int NhaXeId, int NhanVienId, Decimal giave, bool isVeDi = true)
        {
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == giave).FirstOrDefault();
            if (menhgia == null)
                return null;
            //lay thong tin ve dau tien
            var vexeitemfirst = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.NhanVienId == NhanVienId && c.MenhGiaId == menhgia.Id && c.isVeDi == isVeDi && c.NgayBan == null).FirstOrDefault();
            if (vexeitemfirst == null)
                return null;
            return vexeitemfirst;
        }
        public virtual VeXeItem GetVeChuanBiBanTaiQuay(int NhaXeId, int VanPhongId, Decimal giave, bool isVeDi = true)
        {
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == giave).FirstOrDefault();
            if (menhgia == null)
                return null;
            //lay thong tin ve dau tien
            var vexeitemfirst = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.VanPhongId == VanPhongId && c.MenhGiaId == menhgia.Id && c.isVeDi == isVeDi && c.NgayBan == null)
                .OrderBy(q => q.ThuTuBan).ThenBy(m => m.Id)
                .FirstOrDefault();
            if (vexeitemfirst == null)
                return null;
            return vexeitemfirst;
        }
        public virtual int CountVeConLaiTaiQuay(int nhaxeID, int VanPhongId = 0, decimal MenhGia = 0, bool IsVeDi = true)
        {
            var menhgia = _menhgiaveRepository.Table.Where(c => c.MenhGia == MenhGia).FirstOrDefault();
            if (menhgia == null)
                return 0;
            //lay thong tin ve dau tien
            var coutve = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted
                && c.VanPhongId == VanPhongId
                && c.MenhGiaId == menhgia.Id
                && c.isVeDi == IsVeDi
                && c.NgayBan == null).Count();

            return coutve;
        }
        public virtual int SoVeConLaiCuaQuay(int nhaxeID, int VanPhongId = 0)
        {


            var coutve = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted
                && c.VanPhongId == VanPhongId
                && c.NgayBan == null).Count();

            return coutve;
        }
        public virtual decimal DoanhThuQuayHienTai(int nhaxeID, int VanPhongId = 0)
        {

            decimal TongDoanhThu = 0;
            var veitems = _vexeitemRepository.Table.Where(c => c.NhaXeId == nhaxeID && !c.isDeleted
                && c.VanPhongId == VanPhongId
                && c.NgayBan != null
                 && c.NgayBan.Value.Year == DateTime.Now.Year
                 && c.NgayBan.Value.Month == DateTime.Now.Month
                 && c.NgayBan.Value.Day == DateTime.Now.Day);
            foreach (var m in veitems)
            {
                TongDoanhThu = TongDoanhThu + m.GiaVe;
            }

            return TongDoanhThu;
        }
        public virtual void FinishGiaoDichKeVe(int Id)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [FinishGiaoDichKeVe] {0} ", Id), false, 600);
        }
        public virtual void TraVeTheoMenhGia(int Id)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [TraVeTheoMenhGia] {0} ", Id), false, 600);
        }
        public virtual void ChuyenVeTheoMenhGia(int Id, int NhanVienNhanId)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [ChuyenVeTheoMenhGia] {0},{1} ", Id, NhanVienNhanId), false, 600);
        }
        /// <summary>
        /// Huy ban ve
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="NhanVienId"></param>
        /// <param name="VeXeItemId"></param>
        public virtual void HuyBanVe(int NhaXeId, int NhanVienId, int VeXeItemId)
        {
            var vexe = _vexeitemRepository.GetById(VeXeItemId);
            if (vexe != null)
            {
                if (vexe.NhaXeId == NhaXeId && vexe.NhanVienId == NhanVienId)
                {
                    vexe.XeXuatBenId = null;
                    vexe.NguonVeXeId = null;
                    vexe.TrangThai = ENVeXeItemTrangThai.DA_GIAO_HANG;
                    vexe.NgayBan = null;
                    vexe.NgayDi = null;
                    _vexeitemRepository.Update(vexe);
                }

            }
        }
        #region giay di duong
        public int GetSLVeChangDiDuong(DateTime ngaydi, int NguonVeId, int ChangId, int NhaXeId)
        {

            var SoLuongVe = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted
                  && c.NgayBan != null
                 && c.NgayDi.Value.Year == ngaydi.Year
                  && c.NgayDi.Value.Month == ngaydi.Month
                  && c.NgayDi.Value.Day == ngaydi.Day
                  && c.NguonVeXeId.Value == NguonVeId
                  && c.ChangId.Value == ChangId
                  ).Count();

            return SoLuongVe;
        }
        public virtual void DeleteXeXuatBenVeXeItem(int ChuyenDiId, int ChuyenVeId)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [DeleteXeXuatBenVeXeItem] {0},{1} ", ChuyenDiId, ChuyenVeId), false, 600);
        }
        public virtual void InsertXeXuatBenVeXeItem(int XeXuatBenId, int ChangId, int[] vexeitems)
        {
            int? _changid = null;
            if (ChangId > 0)
                _changid = ChangId;
            var items = vexeitems.Select(c => new XeXuatBenVeXeItem
            {
                XeXuatBenId = XeXuatBenId,
                VeXeItemId = c,
                ChangId = _changid
            }).ToList();
            if (items.Count>0)
                _xexuatbenvexeitemRepository.Insert(items);
        }
        public virtual void FinishXuatBenVeXeItem(int ChuyenDiId, int ChuyenVeId)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [FinishXuatBenVeXeItem] {0},{1} ", ChuyenDiId, ChuyenVeId), false, 600);
        }
        public virtual void PhoiVeBoSungThemMoi(int ChuyenDiId, int NhanVienId, int ChangId, int MauVeId, int SeriFrom, int SeriTo,string SeriGiamGia)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [PhoiVeBoSungThemMoi] {0},{1},{2},{3},{4},{5},'{6}'", ChuyenDiId, NhanVienId, ChangId, MauVeId, SeriFrom, SeriTo, SeriGiamGia), false, 600);
        }
        public virtual void PhoiVeBoSungHuy(int ChuyenDiId, int NhanVienId, int ChangId, int MauVeId, int SeriFrom, int SeriTo)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [PhoiVeBoSungHuy] {0},{1},{2},{3},{4},{5}", ChuyenDiId,NhanVienId, ChangId, MauVeId, SeriFrom, SeriTo), false, 600);
        }
        public virtual List<VeXeItem> GetTonVeXeItemsByNhanVien(int NhanVienId, decimal MenhGia, int MauVeId, int SoLuong)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhanVienId == NhanVienId
                && !c.isDeleted
                && c.MauVeKyHieuId == MauVeId
                && c.menhgia.MenhGia == MenhGia
                && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG
                );
            return query.OrderBy(c => c.SoQuyen).ThenBy(c => c.SoSeriNum).Take(SoLuong).ToList();
        }
        #endregion
        #region doanh thu theo xe
        public decimal GetTonglDoanhThuXeTheoNgay(DateTime ngaydi, int XeId, int NguonVeId, out int SoLuong, int MenhGiaId = 0)
        {

            decimal tongdoanhthu = 0;
            var veitems = _vexeitemRepository.Table
                .Join(_nguonvexeRepository.Table, vx => vx.NguonVeXeId, nv => nv.Id, (vx, nv) => new { veitem = vx, NguonVeXe = nv })
               .Join(_hisRepository.Table, pv => pv.NguonVeXe.Id, hisxexuatben => hisxexuatben.NguonVeId, (pv, hisxexuatben) => new { veitem = pv.veitem, NguonVeXe = pv.NguonVeXe, HisXeXuatBen = hisxexuatben })
             .Where(c => (c.HisXeXuatBen.XeVanChuyenId == XeId || XeId == 0)
                  && c.veitem.NgayBan != null && !c.veitem.isDeleted
                 && c.veitem.NgayDi.Value.Year == ngaydi.Year
                  && c.veitem.NgayDi.Value.Month == ngaydi.Month
                  && c.veitem.NgayDi.Value.Day == ngaydi.Day
                  && c.veitem.NguonVeXeId == NguonVeId
                  );

            if (MenhGiaId > 0)
                veitems = veitems.Where(c => c.veitem.MenhGiaId == MenhGiaId);

            foreach (var pv in veitems.ToList())
            {
                tongdoanhthu = tongdoanhthu + pv.veitem.GiaVe;
            }
            SoLuong = veitems.Count();
            return tongdoanhthu;
        }

        #endregion
        #region doanh thu theo hàng ngay theo nhan vien
        public List<ThongKeItem> VTGetDoanhThuBanVeTheoNgay(DateTime tuNgay, DateTime denNgay, int nhaxeid, int VanPhongId)
        {
            denNgay = denNgay.AddDays(1);

            var phoives = _vexeitemRepository.Table
                .Join(_nhanvienRepository.Table, pv => pv.NhanVienId, nhanvien => nhanvien.Id, (pv, nhanvien) => new { veitem = pv, Nhanvien = nhanvien })
               .Where(c => c.veitem.NhaXeId == nhaxeid && !c.veitem.isDeleted
                    && c.veitem.NgayBan != null
                    && (c.veitem.NhanVienId == c.Nhanvien.Id && c.Nhanvien.VanPhongID == VanPhongId)
                    && (c.veitem.NgayBan <= denNgay)
                    && (c.veitem.NgayBan >= tuNgay))
             .Select(c => new
             {
                 GiaVe = c.veitem.GiaVe,
                 NgayBan = c.veitem.NgayBan.Value,
             }).ToList();
            var veitems = phoives.Select(c => new
                 {
                     GiaVe = c.GiaVe,
                     NgayBan = c.NgayBan.Date,
                 })
              .GroupBy(c => new { c.NgayBan })
              .Select(g => new ThongKeItem
              {
                  ItemDataDate = g.Key.NgayBan,
                  GiaTri = g.Sum(a => a.GiaVe),
                  SoLuong = g.Count()
              })
              .OrderByDescending(sx => sx.ItemDataDate)
              .ToList();


            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in veitems)
            {
                item.Nhan = item.ItemDataDate.ToString("dd-MM-yyyy");
                item.NhanSapXep = item.ItemDataDate.ToString("yyyyMMdd");
                tknhanvien.Add(item);
            }
            return tknhanvien;

        }
        public List<ThongKeItem> VTGetDoanhThuBanVeTheoNhanVien(int nhaxeid, int VanPhongId, DateTime NgayBan)
        {
            var phoives = _vexeitemRepository.Table
                           .Join(_nhanvienRepository.Table, pv => pv.NhanVienId, nhanvien => nhanvien.Id, (pv, nhanvien) => new { veitem = pv, Nhanvien = nhanvien })
                          .Where(c => c.veitem.NhaXeId == nhaxeid && !c.veitem.isDeleted
                               && c.veitem.NgayBan != null
                    && (c.veitem.NgayBan.Value.Year == NgayBan.Year)
                     && (c.veitem.NgayBan.Value.Month == NgayBan.Month)
                     && (c.veitem.NgayBan.Value.Day == NgayBan.Day))
             .Select(c => new
             {
                 NhanVienId = c.veitem.NhanVienId.Value,
                 GiaVe = c.veitem.GiaVe

             })
             .GroupBy(c => new { c.NhanVienId })
             .Select(g => new ThongKeItem
             {
                 ItemId = g.Key.NhanVienId,
                 GiaTri = g.Sum(a => a.GiaVe),
                 SoLuong = g.Count()
             })
             .OrderByDescending(sx => sx.ItemId)
             .ToList();
            var tknhanvien = new List<ThongKeItem>();
            foreach (var item in phoives)
            {
                item.Nhan = item.GiaTri.ToString();
                item.NhanSapXep = item.GiaTri.ToString();
                var checknhanvien = _nhanvienRepository.Table.Where(c => c.Id == item.ItemId && c.VanPhongID == VanPhongId).Count();
                if (checknhanvien > 0)
                    tknhanvien.Add(item);
            }
            return tknhanvien;

        }
        /// <summary>
        /// true: co ton tai
        /// false: k ton tai
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="arrSeritext"></param>
        /// <returns></returns>
        public virtual bool KiemTranTonTaiSeri(int NhaXeId, List<string> arrSeritext)
        {
            foreach (var item in arrSeritext)
            {
                string[] arrKhoangSeri = item.Split('-');
                if (arrKhoangSeri.Count() == 1)
                {
                    if (!isExistVeXeItem(item, item))
                    {
                        return false;
                    }
                }
                if (arrKhoangSeri.Count() == 2)
                {
                    var Serifrom = arrKhoangSeri[0];
                    var Serito = arrKhoangSeri[1];
                    if (!isExistVeXeItem(Serifrom, Serito))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public virtual VeXeItem GetVeXeItemBySoSeri(int NhaXeId, int soSeri)
        {
            var query = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.SoSeriNum == soSeri);
            if (query.Count() > 0)
            {
                return query.First();
            }
            return null;
        }
       
        public virtual bool KiemTraSeriQuay(int NhaXeId, string[] arrseri)
        {
            int[] arrsernum = arrseri.Cast<int>().ToArray();
            foreach (var num in arrsernum)
            {
                if (!_vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_BAN && c.SoSeriNum == num).Any())
                    return false;
            }
            return true;
        }
        public List<VeXeItem> VTGetDetailDoanhThu(int nhaxeid, DateTime ngaydi, int nhanvienid = 0)
        {
            var phoives = _vexeitemRepository.Table

              .Where(c => c.NhaXeId == nhaxeid && !c.isDeleted
                  && c.NgayBan != null
                  && c.NgayBan.Value.Year == ngaydi.Year
                  && c.NgayBan.Value.Month == ngaydi.Month
                  && c.NgayBan.Value.Day == ngaydi.Day
                  && ((c.NhanVienId.HasValue && c.NhanVienId == nhanvienid) || nhanvienid == 0)
                  ).ToList();


            return phoives;
        }

        #endregion
        #region doanh thu dinh ki
        public virtual decimal VTDoanhThuTungNhanvien(int NhanVienId, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int SoLuong, int MenhGiaId = 0)
        {
            int Thang1, Thang2;
            ProcessTime(thang, nam, QuyId, LoaiThoiGianId, out Thang1, out Thang2);
            var veitems = _vexeitemRepository.Table.Where(c => c.NhanVienId.HasValue && c.NhanVienId == NhanVienId && !c.isDeleted
                && c.NgayBan != null
                && c.NgayBan.Value.Year == nam
                && (c.NgayBan.Value.Month >= Thang1 && c.NgayBan.Value.Month <= Thang2)
                );
            if (MenhGiaId > 0)
                veitems = veitems.Where(c => c.MenhGiaId == MenhGiaId);
            var doanhthuitems = veitems
               .Select(c => new DoanhThuItem
               {
                   Ngay = c.NgayBan.Value.Day,
                   Thang = c.NgayBan.Value.Month,
                   Nam = c.NgayBan.Value.Year,
                   DoanhThu = c.GiaVe
               }).ToList();
            SoLuong = doanhthuitems.Count;
            return GetTongDoanhThu(doanhthuitems, thang, nam, QuyId, LoaiThoiGianId);
        }


        public virtual void ProcessTime(int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId, out int Thang1, out int Thang2)
        {
            Thang1 = thang;
            Thang2 = thang;

            if (LoaiThoiGianId == ENBaoCaoLoaiThoiGian.TheoQuy)
            {
                switch (QuyId)
                {
                    case ENBaoCaoQuy.Quy1:
                        Thang1 = 1;
                        Thang2 = 3;
                        break;
                    case ENBaoCaoQuy.Quy2:
                        Thang1 = 4;
                        Thang2 = 6;
                        break;
                    case ENBaoCaoQuy.Quy3:
                        Thang1 = 7;
                        Thang2 = 9;
                        break;
                    case ENBaoCaoQuy.Quy4:
                        Thang1 = 10;
                        Thang2 = 12;
                        break;
                }
            }
            else if (LoaiThoiGianId == ENBaoCaoLoaiThoiGian.TheoNam)
            {
                Thang1 = 1;
                Thang2 = 12;
            }
        }
        public virtual decimal GetTongDoanhThu(List<DoanhThuItem> doanhthus, int thang, int nam, ENBaoCaoQuy QuyId, ENBaoCaoLoaiThoiGian LoaiThoiGianId)
        {
            decimal _doanhthu = decimal.Zero;
            foreach (var item in doanhthus)
            {
                switch (LoaiThoiGianId)
                {
                    case ENBaoCaoLoaiThoiGian.TheoThang:
                        {
                            if (item.Nam == nam && item.Thang == thang)
                            {
                                _doanhthu = _doanhthu + item.DoanhThu;

                            }
                            break;
                        }
                    case ENBaoCaoLoaiThoiGian.TheoQuy:
                        {
                            thang = 0;
                            if (item.Nam == nam)
                            {
                                switch (QuyId)
                                {
                                    case ENBaoCaoQuy.Quy1:
                                        if (item.Thang >= 1 && item.Thang <= 3)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy2:
                                        if (item.Thang >= 4 && item.Thang <= 6)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy3:
                                        if (item.Thang >= 7 && item.Thang <= 9)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                    case ENBaoCaoQuy.Quy4:
                                        if (item.Thang >= 10 && item.Thang <= 12)
                                            _doanhthu = _doanhthu + item.DoanhThu;
                                        break;
                                }
                            }
                            break;
                        }
                    case ENBaoCaoLoaiThoiGian.TheoNam:
                        {
                            QuyId = 0;
                            thang = 0;
                            if (item.Nam == nam)
                            {
                                _doanhthu = _doanhthu + item.DoanhThu;
                            }
                            break;
                        }
                }

            }
            return _doanhthu;
        }
        #endregion
        #region Ve Xe Quyen
        /// <summary>
        /// Lay thong tin quyen ve con ton, theo nhân vien hoac van phong
        /// </summary>
        /// <param name="NhaXeId"></param>
        /// <param name="NhanVienId"></param>
        /// <param name="VanPhongId"></param>
        /// <param name="MenhGiaId"></param>
        /// <returns></returns>
        public virtual List<VeXeQuyen> GetTonVeXeQuyen(int NhaXeId, int MenhGiaId, int VanPhongId = 0, int NhanVienId = 0)
        {
            //lay thong tin quyen con ton
            var vexes = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.MenhGiaId == MenhGiaId && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG);
            if (VanPhongId > 0)
            {
                vexes = vexes.Where(c => c.VanPhongId == VanPhongId);
            }
            //neu khong co, thi kiem tra ve tu nhan vien
            if (!vexes.Any() && NhanVienId > 0)
            {
                vexes = _vexeitemRepository.Table.Where(c => c.NhaXeId == NhaXeId && !c.isDeleted && c.MenhGiaId == MenhGiaId && c.TrangThaiId == (int)ENVeXeItemTrangThai.DA_GIAO_HANG);
                vexes = vexes.Where(c => c.NhanVienId == NhanVienId);
            }
            //lay thong tin id cua quyen
            var quyenids = vexes.Select(c => c.QuyenId).Distinct().ToList();
            var quyens = _vexequyenRepository.Table.Where(c => c.NhaXeId == NhaXeId && quyenids.Contains(c.Id)).OrderBy(m => m.ThuTuBan).ToList();
            return quyens;
        }
        public virtual void UpdateQuyenVeThuTuBan(int Id, int ThuThuBan)
        {
            _dbContext.ExecuteSqlCommand(string.Format("EXEC [UpdateVeXeQuyenThuTuBan] {0},{1} ", Id, ThuThuBan), false, 600);

        }
        #endregion

        #region Thiet Dat so serial ve
        void ResetVeXeChuaBan(VeXeItem _vexeitem)
        {
            _vexeitem.TrangThai = ENVeXeItemTrangThai.DA_GIAO_HANG;
            _vexeitem.XeXuatBenId = null;
            _vexeitem.NguonVeXeId = null;
            _vexeitem.NgayBan = null;
            _vexeitem.NgayDi = null;
            _vexeitem.ChangId = null;
            _vexeitem.isGiamGia = false;
            _vexeitem.GiaVe = _vexeitem.menhgia.MenhGia;
            _vexeitemRepository.Update(_vexeitem);
        }
        public virtual bool GanSoSerial(int PhoiVeId, int NhanVienId, int QuayBanVeId, int MauVeKyHieuId, string MaVe)
        {
            var _phoiveitem = _phoiveRepository.GetById(PhoiVeId);
            if (_phoiveitem == null)
                return false;
            if(string.IsNullOrEmpty(MaVe))
            {
                //huy viec gan ve
                if(_phoiveitem.VeXeItemId.HasValue)
                {
                    var _vexeitem = GetVeXeItemById(_phoiveitem.VeXeItemId.Value);
                    if (_vexeitem == null)
                        return false;
                    //dua ve cu ve trang thai chua su dung
                    ResetVeXeChuaBan(_vexeitem);
                }
                return true;
            }
            int MaveNum;
            if(!int.TryParse(MaVe,out MaveNum))
                MaveNum=-1;
            //kiem tra ve hop le ko ?
            var vexeitem = _vexeitemRepository.Table.Where(c => c.MauVeKyHieuId == MauVeKyHieuId && c.SoSeriNum == MaveNum && c.VanPhongId == QuayBanVeId && c.TrangThaiId != (int)ENVeXeItemTrangThai.DA_SU_DUNG).FirstOrDefault();
            if (vexeitem == null)
                return false;
            //kiem tra menh gia co khop voi nhau ko 
            if (vexeitem.GiaVe != _phoiveitem.changgiave.GiaVe)
                return false;
            //Ok update phoi ve
            //kiem tra phoi ve da update ve trc do chua ?
            if(_phoiveitem.VeXeItemId.HasValue)
            {
                //da cap nhat roi thi ko update nua
                if (vexeitem.Id == _phoiveitem.VeXeItemId)
                    return true;
                //neu ton tai roi, => dang update lai ve cu -> ve moi
                var _vexeitem = GetVeXeItemById(_phoiveitem.VeXeItemId.Value);
                if (_vexeitem == null)
                    return false;
                //dua ve cu ve trang thai chua su dung
                ResetVeXeChuaBan(_vexeitem);
            
            }
            
            //cap nhat phoi ve vao ve xem moi
            _phoiveitem.MaVe = vexeitem.SoSeri;
            _phoiveitem.VeXeItemId = vexeitem.Id;
            _phoiveitem.TrangThai = ENTrangThaiPhoiVe.DaGiaoHang;
            _phoiveRepository.Update(_phoiveitem);    
            //cap nhat ve moi 
            vexeitem.TrangThai = ENVeXeItemTrangThai.DA_SU_DUNG;
            vexeitem.XeXuatBenId = _phoiveitem.ChuyenDiId;
            vexeitem.NguonVeXeId = _phoiveitem.NguonVeXeId;
            vexeitem.NgayBan = _phoiveitem.NgayDi;
            vexeitem.NgayDi = _phoiveitem.NgayDi;
            vexeitem.ChangId = _phoiveitem.ChangId;
            vexeitem.isGiamGia = _phoiveitem.IsForKid;
            vexeitem.GiaVe = _phoiveitem.GiaVeHienTai;
            _vexeitemRepository.Update(vexeitem);
            return true;           
            
        }
        #endregion
        #region Chuyen di tai chinh: thu chi , doanh thu, hoa hong
        public void InsertChuyenDiTaiChinh(ChuyenDiTaiChinh item)
        {
            _chuyenditaichinhRepository.Insert(item);
        }
        public void UpdateChuyenDiTaiChinh(ChuyenDiTaiChinh item)
        {
            _chuyenditaichinhRepository.Update(item);
        }
        public void DeleteChuyenDiTaiChinh(ChuyenDiTaiChinh item)
        {
            _chuyenditaichinhRepository.Delete(item);
        }
        public ChuyenDiTaiChinh GetChuyenDiTaiChinhById(int Id)
        {
            return _chuyenditaichinhRepository.GetById(Id);
        }
        
        
        public ChuyenDiTaiChinh GetChuyenDiTaiChinhByLuotId(int LuotId)
        {
            return _chuyenditaichinhRepository.Table.Where(c => c.LuotDiId == LuotId || c.LuotVeId == LuotId).FirstOrDefault();
        }

        public void InsertChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item)
        {
            _chuyenditaichinhthuchiRepository.Insert(item);
        }
        public void UpdateChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item)
        {
            _chuyenditaichinhthuchiRepository.Update(item);
        }
        public void DeleteChuyenDiTaiChinhThuChi(ChuyenDiTaiChinhThuChi item)
        {
            _chuyenditaichinhthuchiRepository.Delete(item);
        }
        public void DeleteAllChuyenDiTaiChinhThuChi(List<ChuyenDiTaiChinhThuChi> item)
        {
            foreach (var m in item)
            {
                _chuyenditaichinhthuchiRepository.Delete(m);
            }

        }
        public ChuyenDiTaiChinhThuChi GetChuyenDiTaiChinhThuChiById(int ChuyenDiTaiChinhId, int LoaiThuChiId)
        {
            return _chuyenditaichinhthuchiRepository.Table.Where(c => c.ChuyenDiTaiChinhId == ChuyenDiTaiChinhId && c.LoaiThuChiId == LoaiThuChiId).FirstOrDefault();
        }
        
        
        #endregion
    }
}

