# 🚀 Dasseti Investment Analysis API

## 📋 Project Overview
A comprehensive RESTful API for investment analysis, ESG scoring, and due diligence management. Built with .NET 8 and PostgreSQL, featuring AI-powered company analysis capabilities.

## 🎯 Key Features
- ✅ Complete CRUD operations for company management
- ✅ ESG scoring and top performers tracking
- ✅ AI-powered investment analysis and risk assessment
- ✅ Due diligence data management
- ✅ RESTful API design with Swagger documentation
- ✅ PostgreSQL database with Entity Framework Core

## 🛠️ Technologies Used
- **.NET 8** - Modern C# framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database operations
- **PostgreSQL** - Robust relational database
- **Semantic Kernel** - AI integration capabilities
- **Swagger/OpenAPI** - API documentation and testing

## 🏗️ Architecture
- **Models**: Company data structures and DTOs
- **Controllers**: RESTful API endpoints
- **Services**: Business logic and AI analysis
- **Data**: Database context and configurations

## 🚀 API Endpoints

### Companies Management
- `GET /api/companies` - Get all companies
- `GET /api/companies/{id}` - Get specific company
- `POST /api/companies` - Create new company
- `PUT /api/companies/{id}` - Update company
- `DELETE /api/companies/{id}` - Delete company

### ESG & Analysis
- `GET /api/companies/esg-performers` - Get top ESG performers
- `POST /api/companies/{id}/analyze` - Generate AI analysis

## 📊 Sample Data
The API includes sample companies (Apple, Tesla) with:
- Market capitalization data
- Revenue information
- ESG scores
- Risk assessments
- Industry classifications

## 🔧 Setup Instructions

### Prerequisites
- .NET 8 SDK
- PostgreSQL 15+
- Visual Studio 2022

### Installation
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run `dotnet restore` to install packages
4. Run `dotnet run` to start the application
5. Navigate to `https://localhost:7xxx/swagger` for API documentation

## 🧪 Testing
Use the Swagger UI for interactive API testing, or tools like Postman for comprehensive testing scenarios.

## 🎯 Business Value
This API demonstrates:
- **ESG Integration**: Critical for modern investment decisions
- **AI-Powered Analysis**: Automated investment recommendations
- **Scalable Architecture**: Ready for enterprise deployment
- **Due Diligence Support**: Comprehensive data management
- **Compliance Ready**: Structured data for regulatory requirements

## 👨‍💻 Developer
Built as a demonstration of modern API development practices for investment technology applications.

## 📞 Contact
Ahmed Khan - ahmed2004.akn@gmail.com - ahmedkhan2004