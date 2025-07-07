using Microsoft.EntityFrameworkCore;
using DassetiInvestmentAPI.Models;

namespace DassetiInvestmentAPI.Data
{
    public class InvestmentDbContext : DbContext
    {
        public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Symbol).IsRequired().HasMaxLength(10);
                entity.Property(e => e.MarketCap).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Revenue).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ESGScore).HasColumnType("decimal(5,2)");
                entity.HasIndex(e => e.Symbol).IsUnique();
            });

            // Sample data for testing
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Apple Inc.",
                    Symbol = "AAPL",
                    Industry = "Technology",
                    Sector = "Consumer Electronics",
                    MarketCap = 3000000000000m,
                    Country = "United States",
                    Revenue = 394328000000m,
                    ESGScore = 82.5m,
                    RiskLevel = "Low"
                },
                new Company
                {
                    Id = 2,
                    Name = "Tesla Inc.",
                    Symbol = "TSLA",
                    Industry = "Automotive",
                    Sector = "Electric Vehicles",
                    MarketCap = 800000000000m,
                    Country = "United States",
                    Revenue = 96773000000m,
                    ESGScore = 78.2m,
                    RiskLevel = "Medium"
                }
            );
        }
    }
}