using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _service;
        public AreaController(IAreaService service)
        {
            _service = service;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult<PaginatedList<AreaDTO>>> GetList(int page = 1, string search = "")
        {
            return await _service.GetAreaList(page, search);
        }

        // PUT: api/Area/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AreaDTO area)
        {
            try
            {
                if (id != area.AreaId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveArea(area));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Area
        [HttpPost]
        public async Task<ActionResult<CmmArea>> Post(AreaDTO area)
        {
            return Ok(await _service.SaveArea(area));
        }

        // DELETE: api/Area/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteArea(id));
        }
    }
}
