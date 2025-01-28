using Application.Contracts.Services;
using Application.Models.Structs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportPenaltyController : ControllerBase
    {

        private readonly IReportPenaltyService _service;
        public ReportPenaltyController(IReportPenaltyService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GenerateReport(Client clientInfo)
        {
            return Ok(await _service.GenerateReport(clientInfo));
        }
    }
}
