using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UnionMall.Configuration;
using UnionMall.Web;

namespace UnionMall.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class UnionMallDbContextFactory : IDesignTimeDbContextFactory<UnionMallDbContext>
    {
        public UnionMallDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UnionMallDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            UnionMallDbContextConfigurer.Configure(builder, configuration.GetConnectionString(UnionMallConsts.ConnectionStringName));

            return new UnionMallDbContext(builder.Options);
        }
    }
}
