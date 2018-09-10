using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;

namespace UnionMall.Controllers
{
    public abstract class UnionMallControllerBase : AbpController
    {

        protected UnionMallControllerBase()
        {
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;

        }


        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
