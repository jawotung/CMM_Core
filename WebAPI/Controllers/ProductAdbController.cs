using Application.Contracts.Services;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAdbController : ControllerBase
    {
        private readonly IProductAdbService _service;
        public ProductAdbController(IProductAdbService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductAdbDTO>>> GetList(int ProductId, int page = 1,string search = "")
        {
            return await _service.GetProductAdbList(ProductId, page, search);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductAdbDTO productAdb)
        {
            try
            {
                if (id != productAdb.ProductAdbId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveProductAdb(productAdb));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<CmmProduct>> Post(ProductAdbDTO productAdb)
        {
            return Ok(await _service.SaveProductAdb(productAdb));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProductAdb(id));
        }
    }
}
