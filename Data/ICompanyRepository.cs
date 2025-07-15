using DassetiInvestmentAPI.Models;

namespace DassetiInvestmentAPI.Data
{
    /// <summary>
    /// Company-specific repository interface with business-specific operations
    /// </summary>
    public interface ICompanyRepository : IRepository<Company>
    {
        // Business-specific queries
        Task<IEnumerable<Company>> GetTopESGPerformersAsync(int count = 5);
        Task<IEnumerable<Company>> GetCompaniesByIndustryAsync(string industry);
        Task<IEnumerable<Company>> GetCompaniesByRiskLevelAsync(string riskLevel);
        Task<IEnumerable<Company>> GetCompaniesByCountryAsync(string country);
        
        // Advanced filtering
        Task<IEnumerable<Company>> GetCompaniesWithESGScoreAboveAsync(decimal minScore);
        Task<IEnumerable<Company>> GetCompaniesWithMarketCapAboveAsync(decimal minMarketCap);
        
        // Business logic
        Task<Company?> GetCompanyBySymbolAsync(string symbol);
        Task<bool> IsSymbolUniqueAsync(string symbol, int? excludeId = null);
        
        // Analytics
        Task<decimal> GetAverageESGScoreAsync();
        Task<decimal> GetAverageMarketCapAsync();
        Task<int> GetTotalCompaniesCountAsync();
    }
}