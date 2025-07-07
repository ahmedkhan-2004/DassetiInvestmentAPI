using DassetiInvestmentAPI.Models;

namespace DassetiInvestmentAPI.Services
{
    public class AIAnalysisService
    {
        private readonly ILogger<AIAnalysisService> _logger;

        public AIAnalysisService(ILogger<AIAnalysisService> logger)
        {
            _logger = logger;
        }

        public async Task<AnalysisResult> AnalyzeCompanyAsync(Company company)
        {
            _logger.LogInformation($"🤖 Starting AI analysis for: {company.Name}");
            
            // Simulate AI processing
            await Task.Delay(1000);
            
            return new AnalysisResult
            {
                RiskAssessment = GenerateRiskAssessment(company),
                ESGAnalysis = GenerateESGAnalysis(company),
                InvestmentRecommendation = GenerateInvestmentRecommendation(company),
                AnalysisDate = DateTime.UtcNow
            };
        }

        private string GenerateRiskAssessment(Company company)
        {
            return company.RiskLevel.ToLower() switch
            {
                "low" => $"✅ {company.Name} shows strong financial stability with low volatility expected.",
                "medium" => $"⚠️ {company.Name} presents moderate risk factors requiring monitoring.",
                "high" => $"🚨 {company.Name} shows higher risk profile requiring careful consideration.",
                _ => $"📊 {company.Name} requires detailed risk assessment."
            };
        }

        private string GenerateESGAnalysis(Company company)
        {
            return company.ESGScore switch
            {
                >= 80 => $"🌟 Excellent ESG performance! {company.Name} scores {company.ESGScore}/100.",
                >= 70 => $"✅ Strong ESG credentials. {company.Name} scores {company.ESGScore}/100.",
                >= 60 => $"⚠️ Moderate ESG performance. {company.Name} scores {company.ESGScore}/100.",
                _ => $"🔴 ESG concerns. {company.Name} scores {company.ESGScore}/100."
            };
        }

        private string GenerateInvestmentRecommendation(Company company)
        {
            var score = (int)((company.ESGScore * 0.6m) + (company.RiskLevel == "Low" ? 40 : company.RiskLevel == "Medium" ? 25 : 10));
            
            return score switch
            {
                >= 80 => $"🚀 STRONG BUY: {company.Name} shows excellent fundamentals.",
                >= 70 => $"📈 BUY: {company.Name} presents good investment opportunity.",
                >= 60 => $"🤝 HOLD: {company.Name} shows stable performance.",
                _ => $"⚠️ REVIEW: {company.Name} requires detailed due diligence."
            };
        }
    }
}