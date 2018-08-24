using Abp.AspNetCore.Mvc.ViewComponents;

namespace UnionMall.Web.Views
{
    public abstract class UnionMallViewComponent : AbpViewComponent
    {
        protected UnionMallViewComponent()
        {
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
    }
}
