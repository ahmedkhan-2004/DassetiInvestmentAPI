using Microsoft.EntityFrameworkCore;
using DassetiInvestmentAPI.Data;
using DassetiInvestmentAPI.Services;
using DassetiInvestmentAPI.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Dasseti Investment API",
        Version = "v1",
        Description = "Professional Investment Analysis API with ESG scoring and AI-powered insights"
    });
});

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Database=DassetiInvestmentDB;Username=postgres;Password=your_password";

builder.Services.AddDbContext<InvestmentDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Repository pattern registration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Business services
builder.Services.AddScoped<AIAnalysisService>();
builder.Services.AddScoped<DataSeedingService>();
builder.Services.AddScoped<MCPServerService>();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString, name: "PostgreSQL");

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dasseti Investment API v1");
        // Swagger UI will now be available at /swagger/index.html
        // Do not set RoutePrefix = string.Empty
    });
}

// Optional redirect: redirect root '/' to Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Health check endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                name = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description,
                duration = x.Value.Duration.ToString()
            })
        });
        await context.Response.WriteAsync(response);
    }
});

// MCP Server endpoints
app.MapGet("/mcp/capabilities", async (MCPServerService mcpService) =>
{
    return await mcpService.GetServerCapabilitiesAsync();
});

app.MapPost("/mcp/execute", async (MCPServerService mcpService, Dictionary<string, object> request) =>
{
    var toolName = request.GetValueOrDefault("tool", "").ToString();
    if (string.IsNullOrEmpty(toolName))
    {
        throw new ArgumentNullException(nameof(toolName), "Tool name must be specified");
    }
    var parameters = request.GetValueOrDefault("parameters", new Dictionary<string, object>()) as Dictionary<string, object> ?? new();
    
    return await mcpService.ExecuteToolAsync(toolName, parameters);
});

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InvestmentDbContext>();
    var seedingService = scope.ServiceProvider.GetRequiredService<DataSeedingService>();
    
    try
    {
        Console.WriteLine("🔄 Initializing database...");
        
        // Create database if it doesn't exist
        await context.Database.EnsureCreatedAsync();
        
        // Run migrations if needed
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }
        
        // Seed initial data
        await seedingService.SeedDataAsync();
        
        Console.WriteLine("✅ Database initialized and seeded successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database initialization failed: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
        }
        
        // Don't throw in production, just log
        if (app.Environment.IsDevelopment())
        {
            throw;
        }
    }
}

var port = app.Urls.FirstOrDefault() ?? "https://localhost:5001";
Console.WriteLine("🚀 Dasseti Investment API is running!");
Console.WriteLine($"📊 Swagger UI available at: {port}/swagger/index.html");
Console.WriteLine($"🔍 Health check at: {port}/health");
Console.WriteLine($"🔌 MCP Server at: {port}/mcp/capabilities");

app.Run();
