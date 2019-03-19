using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using UnionMall.Configuration.Dto;

namespace UnionMall.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : UnionMallAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        { 
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
 