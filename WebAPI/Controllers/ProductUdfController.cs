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
    public class ProductUdfController : ControllerBase
    {
        private readonly IProductUdfService _service;
        public ProductUdfController(IProductUdfService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductUdfDTO>>> GetList(int ProductId, int page = 1,string search = "")
        {
            return await _service.GetProductUdfList(ProductId, page, search);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductUdfDTO productUdf)
        {
            try
            {
                if (id != productUdf.UdfItemId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveProductUdf(productUdf));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<CmmProduct>> Post(ProductUdfDTO productUdf)
        {
            return Ok(await _service.SaveProductUdf(productUdf));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _service.DeleteProductUdf(id));
        }
    }
}
