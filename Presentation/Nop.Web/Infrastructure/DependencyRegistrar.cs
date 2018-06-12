using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Chat;
using Nop.Services.Chonves;
using Nop.Services.ChuyenPhatNhanh;
using Nop.Services.NhaXes;
using Nop.Web.Controllers;
using Nop.Web.Infrastructure.Installation;

namespace Nop.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //we cache presentation models between requests
            builder.RegisterType<BlogController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<CatalogController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<CountryController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<CommonController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<NewsController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<PollController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<ProductController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<ShoppingCartController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<TopicController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<WidgetController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));

            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<ChonVeService>().As<IChonVeService>().InstancePerLifetimeScope();
            builder.RegisterType<NhaXeService>().As<INhaXeService>().InstancePerLifetimeScope();
            builder.RegisterType<DiaChiService>().As<IDiaChiService>().InstancePerLifetimeScope();
            builder.RegisterType<NhanVienService>().As<INhanVienService>().InstancePerLifetimeScope();
            builder.RegisterType<PhieuGuiHangService>().As<IPhieuGuiHangService>().InstancePerLifetimeScope();
            builder.RegisterType<HangHoaService>().As<IHangHoaService>().InstancePerLifetimeScope();
            builder.RegisterType<ChatService>().As<IChatService>().InstancePerLifetimeScope();
            builder.RegisterType<XeInfoService>().As<IXeInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<HanhTrinhService>().As<IHanhTrinhService>().InstancePerLifetimeScope();
            builder.RegisterType<QuanHuyenService>().As<IQuanHuyenService>().InstancePerLifetimeScope();
            builder.RegisterType<BenXeService>().As<IBenXeService>().InstancePerLifetimeScope();
            builder.RegisterType<VeXeService>().As<IVeXeService>().InstancePerLifetimeScope();
            builder.RegisterType<PhoiVeService>().As<IPhoiVeService>().InstancePerLifetimeScope();
            builder.RegisterType<NhaXeCustomerService>().As<INhaXeCustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<GiaoDichKeVeXeService>().As<IGiaoDichKeVeXeService>().InstancePerLifetimeScope();
            builder.RegisterType<BaoCaoService>().As<IBaoCaoService>().InstancePerLifetimeScope();
            builder.RegisterType<KeToanService>().As<IKeToanService>().InstancePerLifetimeScope();
            builder.RegisterType<PhieuChuyenPhatService>().As<IPhieuChuyenPhatService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 2; }
        }
    }
}
