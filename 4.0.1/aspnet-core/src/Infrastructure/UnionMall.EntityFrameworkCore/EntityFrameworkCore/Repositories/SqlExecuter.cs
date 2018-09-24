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

        }
        public DataSet GetCategoryDropDownList(int?tenantId, int parentId = 0, int type = 0)
        {
            DataSet ds = new DataSet();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            string connectionString = configuration.GetConnectionString(UnionMallConsts.ConnectionStringName);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("usp_SelectGoodsCategory", conn);
                comm.CommandTimeout = 60;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@TenantId", tenantId);
                comm.Parameters.AddWithValue("@ParentId", parentId);
                comm.Parameters.AddWithValue("@Type", type);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);
            }
            return ds;
        }

        public DataSet GetPaged(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            total = 0;
            DataSet ds = new DataSet();

            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            string connectionString = configuration.GetConnectionString(UnionMallConsts.ConnectionStringName);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("Global_GetPaged", conn);
                comm.CommandTimeout = 60;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@PageIndex", pageIndex);
                comm.Parameters.AddWithValue("@PageSize", pageSize);
                comm.Parameters.AddWithValue("@Table", table);
                comm.Parameters.AddWithValue("@OrderBy", orderBy);
                //comm.Parameters.AddWithValue("@Where", where);
                comm.Parameters.Add("@TotalCount", SqlDbType.BigInt, 10);
                comm.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@Descript", SqlDbType.VarChar, 500);
                comm.Parameters["@Descript"].Direction = ParameterDirection.Output;
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);
                if (comm.Parameters["@Descript"].Value.ToString() != "successful")
                {
                    throw new Exception(comm.Parameters["@Descript"].Value.ToString());
                }
                int.TryParse(comm.Parameters["@TotalCount"].Value.ToString(), out total);
            }
            return ds;
        }
    }
}
