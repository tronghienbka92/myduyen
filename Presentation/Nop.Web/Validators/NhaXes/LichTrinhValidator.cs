using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.NhaXes;

namespace Nop.Web.Validators.NhaXes
{
    public class LichTrinhValidator : BaseNopValidator<LichTrinhModel>
    {
        public LichTrinhValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.MaLichTrinh).NotEmpty().WithMessage(localizationService.GetResource("ChonVe.NhaXe.LichTrinh.MaLichTrinh.ChuaNhap"));
            RuleFor(x => x.GiaVeToanTuyen).GreaterThanOrEqualTo(0).WithMessage(localizationService.GetResource("ChonVe.NhaXe.LichTrinh.GiaVeToanTuyen.LonHonZero"));
            //RuleFor(x => x.TimeOpenOnline).GreaterThanOrEqualTo(3).WithMessage(localizationService.GetResource("ChonVe.NhaXe.LichTrinh.TimeOpenOnline.ToiThieu"));            
            //RuleFor(x => x.TimeCloseOnline).GreaterThanOrEqualTo(2).WithMessage(localizationService.GetResource("ChonVe.NhaXe.LichTrinh.TimeCloseOnline.ToiThieu"));
        }
    }
}