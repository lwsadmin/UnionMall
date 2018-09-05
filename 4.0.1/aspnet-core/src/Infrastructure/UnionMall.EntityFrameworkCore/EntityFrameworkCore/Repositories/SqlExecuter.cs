using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnionMall.IRepositorySql;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UnionMall.Configuration;
using UnionMall.Web;

namespace UnionMall.EntityFrameworkCore.Repositories
{
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<UnionMallDbContext> _dbContextProvider;

        public SqlExecuter(IDbContextProvider<UnionMallDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }
        public int Execute(string sql, params object[] parameters)
        {
            return _dbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }

        public DataSet ExecuteDataSet(string sql, params object[] parameters)
        {
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());


            string connectionString = configuration.GetConnectionString(UnionMallConsts.ConnectionStringName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                // 填充DataSet. 
                da.Fill(ds);
                cmd.Parameters.Clear();
                connection.Close();
                return ds;

            }


            throw new NotImplementedException();
        }
    }
}
