using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace UnionMall.Web.Views
{
    public abstract class UnionMallRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected UnionMallRazorPage()
        {
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
    }
}
