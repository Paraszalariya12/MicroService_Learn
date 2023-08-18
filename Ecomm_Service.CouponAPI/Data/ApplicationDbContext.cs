using Ecomm_Service.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Service.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "50OFF",
                DiscountAmount = 50,
                MinAmount = 20
            },new Coupon
            {
                CouponId = 2,
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 5
            });
        }

        public DbSet<Coupon> coupons { get; set; }
    }

}
