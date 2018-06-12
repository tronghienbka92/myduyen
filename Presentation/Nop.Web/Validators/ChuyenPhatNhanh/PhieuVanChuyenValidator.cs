using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.ChuyenPhatNhanh;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.ChuyenPhatNhanh
{
    public class PhieuVanChuyenValidator   : BaseNopValidator<PhieuVanChuyenModel>
    {
        public PhieuVanChuyenValidator(ILocalizationService localizationService)
        {
            //RuleFor(x => x.NguoiGui.HoTen).NotEmpty().WithMessage(localizationService.GetResource("Chưa nhập họ tên người gửi "));
            //RuleFor(x => x.NguoiGui.SoDienThoai).NotEmpty().WithMessage(localizationService.GetResource("Chưa nhập số điện thoại người gửi"));
            //RuleFor(x => x.NguoiNhan.HoTen).NotEmpty().WithMessage(localizationService.GetResource("Chưa nhập họ tên người nhận"));
            //RuleFor(x => x.NguoiNhan.SoDienThoai).NotEmpty().WithMessage(localizationService.GetResource("Chưa nhập số điện thoại người nhận "));
        }
    }
}