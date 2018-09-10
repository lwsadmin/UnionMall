using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace UnionMall.IRepositorySql
{
    public interface ISqlExecuter
    {
        int Execute(string sql, params object[] parameters);

        DataSet ExecuteDataSet(string sql, params object[] parameters);

        DataSet GetPaged(int pageIndex, int pageSize, string table, string orderBy, out int total);
    }
}
