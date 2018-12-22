using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using UnionMall.Authorization.Roles;
using UnionMall.Authorization.Users;
using UnionMall.MultiTenancy;
using UnionMall.Entity;
namespace UnionMall.EntityFrameworkCore
{
    public class UnionMallDbContext : AbpZeroDbContext<Tenant, Role, User, UnionMallDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Goods.Category.GoodsCategory> GoodsCategory { get; set; }
        public DbSet<Goods.Brand.Brand> Brand { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<ChainStore> ChainStore { get; set; }
        public DbSet<MemberLevel> MemberLevel { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<CouponSendStatistics> MemCouponSendStatisticsberLevel { get; set; }
        public DbSet<CouponUsedStatistics> CouponUsedStatistics { get; set; }
        public DbSet<CommonCategory> CommonCategory { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ConsumeNote> ConsumeNote { get; set; }
        public DbSet<BalanceNote> BalanceNote { get; set; }
        public DbSet<IntegralNote> IntegralNote { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<GiftOrder> GiftOrder { get; set; }
        public DbSet<GiftOrderItem> GiftOrderItem { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<FlashSale> FlashSale { get; set; }
        public DbSet<FlashSaleOrder> FlashSaleOrder { get; set; }
        public DbSet<FlashSaleOrderItem> FlashSaleOrderItem { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<Advert> Advert { get; set; }
        public DbSet<WechatMessage> WechatMessage { get; set; }
        public DbSet<RechargeNote> RechargeNote { get; set; }
        public DbSet<LevelUpdate> LevelUpdate { get; set; }
        public DbSet<SmsTemplate> SmsTemplate { get; set; }
        public DbSet<AliPayNote> AliPayNote { get; set; }
        public DbSet<WeChatPayNote> WeChatPayNote { get; set; }
        public DbSet<OperateModule> OperateModule { get; set; }
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
