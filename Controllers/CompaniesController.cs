using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DassetiInvestmentAPI.Data;
using DassetiInvestmentAPI.Models;
using DassetiInvestmentAPI.Services;

namespace DassetiInvestmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly InvestmentDbContext _context;
        private readonly AIAnalysisService _aiService;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(InvestmentDbContext context, AIAnalysisService aiService, ILogger<CompaniesController> logger)
        {
            _context = context;
            _aiService = aiService;
            _logger = logger;
        }

        /// <summary>
        /// Get all companies
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            _logger.LogInformation("📊 Fetching all companies");
            return await _context.Companies.ToListAsync();
        }

        /// <summary>
        /// Get company by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found");
            }
            return company;
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(CompanyCreateDto companyDto)
        {
            var company = new Company
            {
                Name = companyDto.Name,
                Symbol = companyDto.Symbol.ToUpper(),
                Industry = companyDto.Industry,
                Sector = companyDto.Sector,
                MarketCap = companyDto.MarketCap,
                Country = companyDto.Country,
                Revenue = companyDto.Revenue,
                ESGScore = companyDto.ESGScore,
                RiskLevel = companyDto.RiskLevel,
                CreatedAt = DateTime.UtcNow
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"✅ Company created: {company.Name}");
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }

        /// <summary>
        /// Update a company
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyCreateDto companyDto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            company.Name = companyDto.Name;
            company.Symbol = companyDto.Symbol.ToUpper();
            company.Industry = companyDto.Industry;
            company.Sector = companyDto.Sector;
            company.MarketCap = companyDto.MarketCap;
            company.Country = companyDto.Country;
            company.Revenue = companyDto.Revenue;
            company.ESGScore = companyDto.ESGScore;
            company.RiskLevel = companyDto.RiskLevel;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"🔄 Company updated: {company.Name}");
            return NoContent();
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"🗑️ Company deleted: {company.Name}");
            return NoContent();
        }

        /// <summary>
        /// Get top ESG performers - Dasseti's specialty!
        /// </summary>
        [HttpGet("esg-performers")]
        public async Task<ActionResult<IEnumerable<Company>>> GetESGTopPerformers()
        {
            _logger.LogInformation("🌱 Fetching top ESG performers");
            var companies = await _context.Companies
                .Where(c => c.ESGScore >= 70)
                .OrderByDescending(c => c.ESGScore)
                .Take(5)
                .ToListAsync();
            
            return Ok(companies);
        }

        /// <summary>
        /// Generate AI analysis for a company
        /// </summary>
        [HttpPost("{id}/analyze")]
        public async Task<ActionResult<AnalysisResult>> AnalyzeCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found");
            }

            _logger.LogInformation($"🤖 Starting AI analysis for: {company.Name}");
            var analysis = await _aiService.AnalyzeCompanyAsync(company);
            
            return Ok(analysis);
        }
    }
}