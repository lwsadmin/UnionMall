using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using UnionMall.Authorization.Roles;
using UnionMall.Authorization.Users;
using UnionMall.MultiTenancy;

namespace UnionMall.EntityFrameworkCore
{
    public class UnionMallDbContext : AbpZeroDbContext<Tenant, Role, User, UnionMallDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public UnionMallDbContext(DbContextOptions<UnionMallDbContext> options)
            : base(options)
        {
        }
    }
}
