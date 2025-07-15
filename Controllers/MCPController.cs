using Microsoft.AspNetCore.Mvc;
using DassetiInvestmentAPI.Services;

namespace DassetiInvestmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MCPController : ControllerBase
    {
        private readonly MCPServerService _mcpService;
        private readonly ILogger<MCPController> _logger;

        public MCPController(MCPServerService mcpService, ILogger<MCPController> logger)
        {
            _mcpService = mcpService;
            _logger = logger;
        }

        /// <summary>
        /// Get MCP server capabilities and schema
        /// </summary>
        [HttpGet("capabilities")]
        public async Task<ActionResult<object>> GetCapabilities()
        {
            _logger.LogInformation("🔌 MCP capabilities requested");
            var capabilities = await _mcpService.GetServerCapabilitiesAsync();
            return Ok(capabilities);
        }

        /// <summary>
        /// Execute MCP tool
        /// </summary>
        [HttpPost("execute")]
        public async Task<ActionResult<object>> ExecuteTool([FromBody] MCPToolRequest request)
        {
            _logger.LogInformation($"🔧 MCP tool execution requested: {request.Tool}");
            
            if (string.IsNullOrEmpty(request.Tool))
            {
                return BadRequest(new { error = "Tool name is required" });
            }

            var result = await _mcpService.ExecuteToolAsync(request.Tool, request.Parameters ?? new());
            return Ok(result);
        }
    }

    public class MCPToolRequest
    {
        public string Tool { get; set; } = string.Empty;
        public Dictionary<string, object>? Parameters { get; set; }
    }
}
