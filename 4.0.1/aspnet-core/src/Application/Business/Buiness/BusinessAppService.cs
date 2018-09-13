using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services;
using UnionMall.IRepositorySql;

namespace UnionMall.Business.Buiness
{
    public class BusinessAppService : ApplicationService, IBusinessAppService
    {
        private readonly ISqlExecuter _sqlExecuter;

        public BusinessAppService(ISqlExecuter sqlExecuter)
        {
            _sqlExecuter = sqlExecuter;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
        }
    }
}
