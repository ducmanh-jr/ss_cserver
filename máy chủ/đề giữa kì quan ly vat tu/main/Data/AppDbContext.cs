using Microsoft.EntityFrameworkCore;
using ConstructionMaterialsApi.Models.Entities;

namespace ConstructionMaterialsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo các bảng trong Database
        public DbSet<Material> Materials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Cấu hình thêm nếu cần (ví dụ: Fluent API)
            modelBuilder.Entity<Material>().Property(m => m.UnitPrice).HasPrecision(18, 2);
        }
    }
}
