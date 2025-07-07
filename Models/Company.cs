namespace DassetiInvestmentAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public decimal MarketCap { get; set; }
        public string Country { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal ESGScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AIAnalysis { get; set; }
        public string? InvestmentRecommendation { get; set; }
    }

    public class CompanyCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public decimal MarketCap { get; set; }
        public string Country { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal ESGScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
    }

    public class AnalysisResult
    {
        public string RiskAssessment { get; set; } = string.Empty;
        public string ESGAnalysis { get; set; } = string.Empty;
        public string InvestmentRecommendation { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }
}