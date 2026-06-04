using Microsoft.EntityFrameworkCore;
using Nguyen_Khanh_Thu_193865.Entites;

namespace ApiWebBasicPlatFrom.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        #region


        public DbSet<Shipper193865De3> Shipper193865De3s { get; set; }
        public DbSet<Product193865De3> Product193865De3s { get; set; }
        public DbSet<ShipperProduct193865De3> ShipperProduct193865De3s { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // cấu hình fluent API
            modelBuilder.Entity<Shipper193865De3>(entity =>
            {
                entity.ToTable("Shipper193865De3");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<Product193865De3>(entity =>
            {
                entity.ToTable("Product193865De3");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<ShipperProduct193865De3>(entity =>
            {
                entity.ToTable("ShipperProduct193865De3");
                entity.HasKey(s => new { s.ShipperID, s.ProductId });

                entity
                    .HasOne(e => e.product193865)
                    .WithMany() //navigation
                    .HasForeignKey(e => e.ProductId)
                    .HasConstraintName("FK_Product193865De3_ShipperProduct193865De3");

                entity
                    .HasOne(e => e.shipper193865)
                    .WithMany()
                    .HasForeignKey(e => e.ShipperID)
                    .HasConstraintName("FK_shipper193865_ShipperProduct193865De3");
            });

        }
    }
}
