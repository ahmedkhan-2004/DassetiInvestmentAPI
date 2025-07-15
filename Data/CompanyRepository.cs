using DassetiInvestmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DassetiInvestmentAPI.Data
{
    /// <summary>
    /// Company repository implementation with business-specific operations
    /// </summary>
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(InvestmentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetTopESGPerformersAsync(int count = 5)
        {
            return await _dbSet
                .Where(c => c.ESGScore >= 70)
                .OrderByDescending(c => c.ESGScore)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByIndustryAsync(string industry)
        {
            return await _dbSet
                .Where(c => c.Industry.ToLower() == industry.ToLower())
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByRiskLevelAsync(string riskLevel)
        {
            return await _dbSet
                .Where(c => c.RiskLevel.ToLower() == riskLevel.ToLower())
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByCountryAsync(string country)
        {
            return await _dbSet
                .Where(c => c.Country.ToLower() == country.ToLower())
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesWithESGScoreAboveAsync(decimal minScore)
        {
            return await _dbSet
                .Where(c => c.ESGScore >= minScore)
                .OrderByDescending(c => c.ESGScore)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesWithMarketCapAboveAsync(decimal minMarketCap)
        {
            return await _dbSet
                .Where(c => c.MarketCap >= minMarketCap)
                .OrderByDescending(c => c.MarketCap)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyBySymbolAsync(string symbol)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Symbol.ToUpper() == symbol.ToUpper());
        }

        public async Task<bool> IsSymbolUniqueAsync(string symbol, int? excludeId = null)
        {
            var query = _dbSet.Where(c => c.Symbol.ToUpper() == symbol.ToUpper());
            
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            
            return !await query.AnyAsync();
        }

        public async Task<decimal> GetAverageESGScoreAsync()
        {
            return await _dbSet.AverageAsync(c => c.ESGScore);
        }

        public async Task<decimal> GetAverageMarketCapAsync()
        {
            return await _dbSet.AverageAsync(c => c.MarketCap);
        }

        public async Task<int> GetTotalCompaniesCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}