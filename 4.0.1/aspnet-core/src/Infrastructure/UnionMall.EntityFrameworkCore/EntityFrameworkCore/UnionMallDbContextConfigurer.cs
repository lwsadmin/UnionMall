using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace UnionMall.EntityFrameworkCore
{
    public static class UnionMallDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<UnionMallDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<UnionMallDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
