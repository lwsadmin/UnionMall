using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UnionMall.Configuration;
using UnionMall.Goods;
namespace UnionMall.Web.Startup
{
    [DependsOn(typeof(UnionMallWebCoreModule))]
    //  DependsOn(typeof(AbpWebApiModule), typeof(AbpProjectTemplateApplicationModule))]
    public class UnionMallWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public UnionMallWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<UnionMallNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(UnionMallWebMvcModule).GetAssembly());

            //
            // Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder.For<IGoodsCategoryAppService>("tasksystem/task").Build();
        }
    }
}
