using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using UnionMall.IRepositorySql;

namespace UnionMall.Goods.GoodsCategory
{
    public class GoodsCategoryAppService: ApplicationService, IGoodsCategoryAppService
    {
        private readonly ISqlExecuter _sqlExecuter;

        public GoodsCategoryAppService(ISqlExecuter sqlExecuter)
        {
            _sqlExecuter = sqlExecuter;
        }
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            DataSet ds = _sqlExecuter.GetPaged(pageIndex, pageSize, table, orderBy, out total);
            return ds;
        }
    }
}
