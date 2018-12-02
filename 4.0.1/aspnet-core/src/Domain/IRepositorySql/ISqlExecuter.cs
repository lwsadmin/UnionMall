using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionMall.IRepositorySql
{
    public interface ISqlExecuter
    {



        int Execute(string sql, params object[] parameters);

        DataSet ExecuteDataSet(string sql, params object[] parameters);

        DataSet GetCategoryDropDownList(int? tenantId, long parentId = 0, int type = 0);

        DataSet GetPaged(int pageIndex, int pageSize, string table, string orderBy, out int total);

        DataSet GetPagedList(int pageIndex, int pageSize, string table, string orderBy, out int total, string idSql = "");
    }
}
