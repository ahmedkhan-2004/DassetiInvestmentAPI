using DassetiInvestmentAPI.Data;
using DassetiInvestmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DassetiInvestmentAPI.Services
{
    /// <summary>
    /// MCP (Model Context Protocol) Server Service
    /// Provides structured access to investment data for AI tools and applications
    /// </summary>
    public class MCPServerService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly AIAnalysisService _aiService;
        private readonly ILogger<MCPServerService> _logger;

        public MCPServerService(
            ICompanyRepository companyRepository,
            AIAnalysisService aiService,
            ILogger<MCPServerService> logger)
        {
            _companyRepository = companyRepository;
            _aiService = aiService;
            _logger = logger;
        }

        /// <summary>
        /// Returns MCP server capabilities and available tools (Async)
        /// </summary>
        public Task<object> GetServerCapabilitiesAsync()
        {
            _logger.LogInformation("🔌 Returning MCP server capabilities");

            var result = new
            {
                serverInfo = new
                {
                    name = "Dasseti Investment API",
                    version = "1.0.0",
                    description = "Professional Investment Analysis API with ESG scoring and AI-powered insights",
                    capabilities = new[]
                    {
                        "investment_analysis",
                        "esg_scoring",
                        "company_data",
                        "risk_assessment"
                    }
                },
                tools = new List<object>
                {
                    new
                    {
                        name = "get_companies",
                        description = "Retrieve all companies in the investment database",
                        parameters = new { }
                    },
                    new
                    {
                        name = "get_company_by_symbol",
                        description = "Get detailed company information by stock symbol",
                        parameters = new
                        {
                            symbol = new { type = "string", description = "Stock symbol (e.g., AAPL, TSLA)" }
                        }
                    },
                    new
                    {
                        name = "get_esg_performers",
                        description = "Get top ESG performing companies",
                        parameters = new
                        {
                            count = new { type = "number", description = "Number of companies to return (default: 5)" }
                        }
                    },
                    new
                    {
                        name = "analyze_company",
                        description = "Get AI-powered analysis for a specific company",
                        parameters = new
                        {
                            symbol = new { type = "string", description = "Stock symbol of company to analyze" }
                        }
                    },
                    new
                    {
                        name = "get_companies_by_risk",
                        description = "Get companies filtered by risk level",
                        parameters = new
                        {
                            riskLevel = new { type = "string", description = "Risk level: Low, Medium, High" }
                        }
                    },
                    new
                    {
                        name = "get_companies_by_industry",
                        description = "Get companies filtered by industry",
                        parameters = new
                        {
                            industry = new { type = "string", description = "Industry name (e.g., Technology, Healthcare)" }
                        }
                    },
                    new
                    {
                        name = "get_market_analytics",
                        description = "Get market analytics and statistics",
                        parameters = new { }
                    }
                }
            };

            return Task.FromResult<object>(result);
        }

        /// <summary>
        /// Executes MCP tool with given parameters
        /// </summary>
        public async Task<object> ExecuteToolAsync(string toolName, Dictionary<string, object> parameters)
        {
            _logger.LogInformation($"🔧 Executing MCP tool: {toolName}");

            try
            {
                return toolName.ToLower() switch
                {
                    "get_companies" => await GetCompaniesAsync(),
                    "get_company_by_symbol" => await GetCompanyBySymbolAsync(parameters),
                    "get_esg_performers" => await GetESGPerformersAsync(parameters),
                    "analyze_company" => await AnalyzeCompanyAsync(parameters),
                    "get_companies_by_risk" => await GetCompaniesByRiskAsync(parameters),
                    "get_companies_by_industry" => await GetCompaniesByIndustryAsync(parameters),
                    "get_market_analytics" => await GetMarketAnalyticsAsync(),
                    _ => new { error = $"Unknown tool: {toolName}", availableTools = GetAvailableTools() }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing MCP tool: {toolName}");
                return new { error = ex.Message, tool = toolName };
            }
        }

        private async Task<object> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllAsync();
            return new
            {
                success = true,
                data = companies,
                count = companies.Count()
            };
        }

        private async Task<object> GetCompanyBySymbolAsync(Dictionary<string, object> parameters)
        {
            if (!parameters.TryGetValue("symbol", out var symbolObj) || string.IsNullOrEmpty(symbolObj?.ToString()))
            {
                return new { error = "Symbol parameter is required" };
            }

            var symbol = symbolObj.ToString()!;
            var company = await _companyRepository.GetCompanyBySymbolAsync(symbol);

            if (company == null)
            {
                return new { error = $"Company with symbol '{symbol}' not found" };
            }

            return new
            {
                success = true,
                data = company
            };
        }

        private async Task<object> GetESGPerformersAsync(Dictionary<string, object> parameters)
        {
            var count = 5;
            if (parameters.TryGetValue("count", out var countObj) && int.TryParse(countObj?.ToString(), out var parsedCount))
            {
                count = parsedCount;
            }

            var companies = await _companyRepository.GetTopESGPerformersAsync(count);
            return new
            {
                success = true,
                data = companies,
                count = companies.Count(),
                description = $"Top {count} ESG performing companies"
            };
        }

        private async Task<object> AnalyzeCompanyAsync(Dictionary<string, object> parameters)
        {
            if (!parameters.TryGetValue("symbol", out var symbolObj) || string.IsNullOrEmpty(symbolObj?.ToString()))
            {
                return new { error = "Symbol parameter is required" };
            }

            var symbol = symbolObj.ToString()!;
            var company = await _companyRepository.GetCompanyBySymbolAsync(symbol);

            if (company == null)
            {
                return new { error = $"Company with symbol '{symbol}' not found" };
            }

            var analysis = await _aiService.AnalyzeCompanyAsync(company);
            return new
            {
                success = true,
                company = new { company.Name, company.Symbol, company.ESGScore, company.RiskLevel },
                analysis = analysis
            };
        }

        private async Task<object> GetCompaniesByRiskAsync(Dictionary<string, object> parameters)
        {
            if (!parameters.TryGetValue("riskLevel", out var riskObj) || string.IsNullOrEmpty(riskObj?.ToString()))
            {
                return new { error = "RiskLevel parameter is required" };
            }

            var riskLevel = riskObj.ToString()!;
            var companies = await _companyRepository.GetCompaniesByRiskLevelAsync(riskLevel);

            return new
            {
                success = true,
                data = companies,
                count = companies.Count(),
                riskLevel = riskLevel
            };
        }

        private async Task<object> GetCompaniesByIndustryAsync(Dictionary<string, object> parameters)
        {
            if (!parameters.TryGetValue("industry", out var industryObj) || string.IsNullOrEmpty(industryObj?.ToString()))
            {
                return new { error = "Industry parameter is required" };
            }

            var industry = industryObj.ToString()!;
            var companies = await _companyRepository.GetCompaniesByIndustryAsync(industry);

            return new
            {
                success = true,
                data = companies,
                count = companies.Count(),
                industry = industry
            };
        }

        private async Task<object> GetMarketAnalyticsAsync()
        {
            var avgESGScore = await _companyRepository.GetAverageESGScoreAsync();
            var avgMarketCap = await _companyRepository.GetAverageMarketCapAsync();
            var totalCompanies = await _companyRepository.GetTotalCompaniesCountAsync();

            var topPerformers = await _companyRepository.GetTopESGPerformersAsync(3);

            return new
            {
                success = true,
                analytics = new
                {
                    totalCompanies = totalCompanies,
                    averageESGScore = Math.Round(avgESGScore, 2),
                    averageMarketCap = Math.Round(avgMarketCap / 1_000_000_000, 2),
                    topESGPerformers = topPerformers.Select(c => new { c.Name, c.Symbol, c.ESGScore }),
                    lastUpdated = DateTime.UtcNow
                }
            };
        }

        private string[] GetAvailableTools()
        {
            return new[]
            {
                "get_companies",
                "get_company_by_symbol",
                "get_esg_performers",
                "analyze_company",
                "get_companies_by_risk",
                "get_companies_by_industry",
                "get_market_analytics"
            };
        }
    }
}
