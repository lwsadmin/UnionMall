using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UnionMall.Configuration;

namespace UnionMall.Web.Host.Startup
{
    [DependsOn(
       typeof(UnionMallWebCoreModule))]
    public class UnionMallWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public UnionMallWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(UnionMallWebHostModule).GetAssembly());
        }
    }
}
