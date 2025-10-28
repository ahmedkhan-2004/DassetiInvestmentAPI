# 📊 Dasseti Investment Intelligence API

> AI-powered investment analysis with **ESG scoring** and **Model Context Protocol (MCP)** integration

## 🎯 Overview

Enterprise API built for institutional investment analysis with AI-powered ESG (Environmental, Social, Governance) scoring and due diligence automation.

## ✨ Features

- **🤖 AI-Ready Architecture**: Built-in MCP (Model Context Protocol) server for AI agent integration
- **📊 Investment Analysis**: ESG scoring, risk assessment, and investment recommendations
- **🔍 Company Data Management**: Comprehensive company information with industry classification
- **💡 Market Analytics**: Real-time market statistics and performance insights
- **🌱 ESG Integration**: Environmental, Social, and Governance scoring system
- **📈 Risk Assessment**: Multi-factor risk evaluation and recommendations

## 🛠️ Tech Stack

- **Backend**: .NET Core 8 Web API
- **Database**: PostgreSQL with Entity Framework Core
- **Documentation**: Swagger/OpenAPI
- **Architecture**: Clean Architecture with Repository Pattern
- **Protocol**: Model Context Protocol (MCP) for AI integration

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- PostgreSQL (local installation)
- Git

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/yourusername/DassetiInvestmentAPI.git
cd DassetiInvestmentAPI
```

2. **Set up PostgreSQL:**
```bash
# Create database (using psql or pgAdmin)
CREATE DATABASE DassetiInvestmentDB;
```

3. **Configure connection string:**
Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=DassetiInvestmentDB;Username=your_username;Password=your_password"
  }
}
```

4. **Restore dependencies:**
```bash
dotnet restore
```

5. **Run the application:**
```bash
dotnet run
```

The API will start at `https://localhost:5001`

## 📚 API Documentation

### Core Endpoints

- **Swagger UI**: `https://localhost:5001/swagger`
- **Health Check**: `https://localhost:5001/health`
- **Root**: `https://localhost:5001/` (redirects to Swagger)

### MCP Endpoints

- **Capabilities**: `GET /mcp/capabilities`
- **Tool Execution**: `POST /mcp/execute`
- **API MCP**: `GET /api/mcp/capabilities`
- **API Tool Execution**: `POST /api/mcp/execute`

## 🔧 MCP Tools Available

| Tool | Description | Parameters |
|------|-------------|------------|
| `get_companies` | Get all companies | None |
| `get_company_by_symbol` | Find company by stock symbol | `symbol` (string) |
| `get_esg_performers` | Top ESG performing companies | `limit` (int, optional) |
| `analyze_company` | AI-powered company analysis | `symbol` (string) |
| `get_companies_by_risk` | Filter companies by risk level | `risk_level` (Low/Medium/High) |
| `get_companies_by_industry` | Filter companies by industry | `industry` (string) |
| `get_market_analytics` | Market statistics and insights | None |

## 🎯 Usage Examples

### Using MCP Tools

**Get Company Analysis:**
```bash
POST /mcp/execute
Content-Type: application/json

{
  "tool": "analyze_company",
  "parameters": {
    "symbol": "AAPL"
  }
}
```

**Get Market Analytics:**
```bash
POST /mcp/execute
Content-Type: application/json

{
  "tool": "get_market_analytics",
  "parameters": {}
}
```

**Filter by Risk Level:**
```bash
POST /mcp/execute
Content-Type: application/json

{
  "tool": "get_companies_by_risk",
  "parameters": {
    "risk_level": "Low"
  }
}
```

## 🗃️ Database Schema

The application automatically creates and seeds the database with:

- **Companies**: Stock information, ESG scores, risk levels
- **Industries**: Sector classification
- **Market Data**: Analytics and performance metrics

### Sample Data Includes:
- Technology companies (Apple, Microsoft, Google)
- Healthcare companies (Johnson & Johnson, Pfizer)
- Financial services (JPMorgan Chase, Bank of America)
- Energy companies (ExxonMobil, Chevron)

## 🏗️ Architecture Overview

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Controllers   │    │    Services     │    │  Repositories   │
│                 │    │                 │    │                 │
│ • MCPController │───▶│ • MCPService    │───▶│ • CompanyRepo   │
│ • BaseController│    │ • AIAnalysis    │    │ • GenericRepo   │
│                 │    │ • DataSeeding   │    │                 │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                                                        │
                                                        ▼
                                              ┌─────────────────┐
                                              │   Database      │
                                              │                 │
                                              │ • PostgreSQL    │
                                              │ • EF Core       │
                                              └─────────────────┘
```

## 🧪 Development

### Running in Development

```bash
# Set development environment
export ASPNETCORE_ENVIRONMENT=Development

# Run with hot reload
dotnet watch run
```

### Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Testing the API

1. **Using Swagger UI**: Navigate to `https://localhost:5001/swagger`
2. **Using curl**: 
```bash
curl -X GET "https://localhost:5001/mcp/capabilities"
```
3. **Using Postman**: Import the API endpoints from Swagger

## 📊 Health Monitoring

The API includes comprehensive health checks:

```bash
GET /health
```

Response includes:
- Database connectivity status
- Application health status
- Response times and diagnostics

## 🔒 Security Features

- **CORS Configuration**: Configurable cross-origin requests
- **Input Validation**: Comprehensive request validation
- **Error Handling**: Structured error responses
- **Connection Security**: Secure database connections

## 📈 Performance Features

- **Async Operations**: Non-blocking database operations
- **Query Optimization**: NoTracking for read operations
- **Connection Pooling**: Efficient database connections
- **Caching Ready**: Architecture supports caching layers

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## 🙏 Acknowledgments

- Built with .NET Core 8
- PostgreSQL for robust data storage
- Entity Framework Core for ORM
- Swagger for API documentation
- Model Context Protocol for AI integration

📞 Support & Contact
Developer: Ahmed Khan
For support and questions:
Create an issue in the GitHub repository
Check the API documentation at /swagger
Review the health endpoint at /health
Email: ahmed2004.akn@gmail.com
LinkedIn: ahmedkhan04
GitHub: ahmedkhan-2004

