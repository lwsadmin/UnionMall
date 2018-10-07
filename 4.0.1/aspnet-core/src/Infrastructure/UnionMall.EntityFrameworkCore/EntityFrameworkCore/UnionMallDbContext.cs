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
        public DbSet<Goods.Category.GoodsCategory> GoodsCategory { get; set; }
        public DbSet<Goods.Brand.Brand> Brand { get; set; }
        public DbSet<Business.Business.Business> Business { get; set; }
        public DbSet<Business.ChainStore.ChainStore> ChainStore { get; set; }
        public UnionMallDbContext(DbContextOptions<UnionMallDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("T");


            //modelBuilder.Entity<Category>().ToTable("GoodsCategory", "T");
            //modelBuilder.Entity<Goods>().ToTable("Goods", "T");
            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
