using Microsoft.EntityFrameworkCore;
using DassetiInvestmentAPI.Models;

namespace DassetiInvestmentAPI.Data
{
    /// <summary>
    /// Database context for Investment API
    /// Manages Company entities and database operations
    /// </summary>
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Company entity
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Required fields
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(10);
                
                entity.Property(e => e.Industry)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Sector)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.RiskLevel)
                    .IsRequired()
                    .HasMaxLength(50);
                
                // Decimal precision
                entity.Property(e => e.MarketCap)
                    .HasColumnType("decimal(18,2)");
                
                entity.Property(e => e.Revenue)
                    .HasColumnType("decimal(18,2)");
                
                entity.Property(e => e.ESGScore)
                    .HasColumnType("decimal(5,2)");
                
                // Optional fields
                entity.Property(e => e.AIAnalysis)
                    .HasMaxLength(1000);
                
                entity.Property(e => e.InvestmentRecommendation)
                    .HasMaxLength(500);
                
                // Indexes
                entity.HasIndex(e => e.Symbol)
                    .IsUnique();
                
                entity.HasIndex(e => e.Industry);
                entity.HasIndex(e => e.ESGScore);
                entity.HasIndex(e => e.RiskLevel);
                
                // Default values
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Remove seed data from here - we'll use DataSeedingService instead
            // This allows for better control over when seeding occurs
        }
    }
}