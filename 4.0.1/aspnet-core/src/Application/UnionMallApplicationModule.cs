using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UnionMall.Authorization;
using Abp.Configuration.Startup;
namespace UnionMall
{
    [DependsOn(
        typeof(UnionMallCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class UnionMallApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<UnionMallAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(UnionMallApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);


            //


            //
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
