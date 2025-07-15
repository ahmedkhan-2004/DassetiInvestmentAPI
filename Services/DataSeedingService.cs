using DassetiInvestmentAPI.Data;
using DassetiInvestmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DassetiInvestmentAPI.Services
{
    /// <summary>
    /// Service for seeding initial data into the database
    /// </summary>
    public class DataSeedingService
    {
        private readonly InvestmentDbContext _context;
        private readonly ILogger<DataSeedingService> _logger;

        public DataSeedingService(InvestmentDbContext context, ILogger<DataSeedingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Seeds initial data if database is empty
        /// </summary>
        public async Task SeedDataAsync()
        {
            _logger.LogInformation("🌱 Starting data seeding process...");

            // Check if data already exists
            if (await _context.Companies.AnyAsync())
            {
                _logger.LogInformation("📊 Data already exists, skipping seeding");
                return;
            }

            var companies = GetSeedCompanies();
            
            _logger.LogInformation($"🌱 Seeding {companies.Count} companies...");
            
            await _context.Companies.AddRangeAsync(companies);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("✅ Data seeding completed successfully!");
        }

        /// <summary>
        /// Returns list of seed companies for initial data
        /// </summary>
        private List<Company> GetSeedCompanies()
        {
            return new List<Company>
            {
                new Company
                {
                    Name = "Apple Inc.",
                    Symbol = "AAPL",
                    Industry = "Technology",
                    Sector = "Consumer Electronics",
                    MarketCap = 3000000000000m,
                    Country = "United States",
                    Revenue = 394328000000m,
                    ESGScore = 82.5m,
                    RiskLevel = "Low",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "Strong technological leadership with excellent ESG practices",
                    InvestmentRecommendation = "STRONG BUY - Consistent growth and innovation"
                },
                new Company
                {
                    Name = "Tesla Inc.",
                    Symbol = "TSLA",
                    Industry = "Automotive",
                    Sector = "Electric Vehicles",
                    MarketCap = 800000000000m,
                    Country = "United States",
                    Revenue = 96773000000m,
                    ESGScore = 78.2m,
                    RiskLevel = "Medium",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "Leading EV manufacturer with strong environmental impact",
                    InvestmentRecommendation = "BUY - High growth potential in sustainable transport"
                },
                new Company
                {
                    Name = "Microsoft Corporation",
                    Symbol = "MSFT",
                    Industry = "Technology",
                    Sector = "Software",
                    MarketCap = 2500000000000m,
                    Country = "United States",
                    Revenue = 211915000000m,
                    ESGScore = 85.0m,
                    RiskLevel = "Low",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "Dominant cloud computing position with excellent ESG credentials",
                    InvestmentRecommendation = "STRONG BUY - Reliable growth and strong fundamentals"
                },
                new Company
                {
                    Name = "Unilever PLC",
                    Symbol = "UL",
                    Industry = "Consumer Goods",
                    Sector = "Personal Care",
                    MarketCap = 150000000000m,
                    Country = "United Kingdom",
                    Revenue = 60069000000m,
                    ESGScore = 88.5m,
                    RiskLevel = "Low",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "ESG leader with strong sustainable business practices",
                    InvestmentRecommendation = "BUY - Excellent ESG profile with stable returns"
                },
                new Company
                {
                    Name = "NextEra Energy Inc.",
                    Symbol = "NEE",
                    Industry = "Utilities",
                    Sector = "Renewable Energy",
                    MarketCap = 160000000000m,
                    Country = "United States",
                    Revenue = 20956000000m,
                    ESGScore = 92.0m,
                    RiskLevel = "Low",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "Leading renewable energy company with top ESG score",
                    InvestmentRecommendation = "STRONG BUY - Perfect ESG investment with growth potential"
                },
                new Company
                {
                    Name = "Saudi Aramco",
                    Symbol = "2222.SR",
                    Industry = "Energy",
                    Sector = "Oil & Gas",
                    MarketCap = 2100000000000m,
                    Country = "Saudi Arabia",
                    Revenue = 535000000000m,
                    ESGScore = 45.0m,
                    RiskLevel = "High",
                    CreatedAt = DateTime.UtcNow,
                    AIAnalysis = "High revenue but significant ESG concerns in fossil fuel sector",
                    InvestmentRecommendation = "HOLD - Monitor ESG improvements and energy transition"
                }
            };
        }
    }
}