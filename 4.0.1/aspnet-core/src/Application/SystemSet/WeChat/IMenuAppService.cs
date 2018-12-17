using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.SystemSet
{
    public interface IMenuAppService: IApplicationService
    {
        Menu GetMenu(string ACCESS_TOKEN);
        MenuError Create(string JsonData);
    }
}
