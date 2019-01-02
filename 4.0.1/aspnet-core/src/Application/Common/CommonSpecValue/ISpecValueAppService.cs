using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Common.Dto;

namespace UnionMall.Common.CommonSpec
{
    public interface ISpecValueAppService : IApplicationService
    {
        Task<List<Entity.CommonSpecValue>> GetSelect();
        Task<List<Entity.CommonSpecValue>> GetBySpecId(long id);
        Task Delete(long id);
    }
}
