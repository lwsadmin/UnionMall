using System.Threading.Tasks;
using UnionMall.Configuration.Dto;

namespace UnionMall.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
