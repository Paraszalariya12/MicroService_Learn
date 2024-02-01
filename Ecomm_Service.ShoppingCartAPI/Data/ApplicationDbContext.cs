using Ecomm_Service.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Service.ShoppingCartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

        public DbSet<CartHeader> cartHeaders { get; set; }
        public DbSet<CartDetail> cartDetails { get; set; }
    }

}
