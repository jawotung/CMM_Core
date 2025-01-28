using Application.Contracts.Services;
using Application.Models.Responses;
using Application.Models.Structs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportPaymentController : ControllerBase
    {

        private readonly IReportPaymentService _service;
        public ReportPaymentController(IReportPaymentService service)
        {
            _service = service;
        }
        public async Task<IActionResult> GenerateReport(bool Selected)
        {
            return Ok(await _service.GenerateReport(Selected));
        }
    }
}
