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
    public class ProductTierController : ControllerBase
    {
        private readonly IProductTierService _service;
        public ProductTierController(IProductTierService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductTierDTO>>> GetList(int ProductAdbId, int page = 1,string search = "")
        {
            return await _service.GetProductTierList(ProductAdbId, page, search);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductTierDTO productTier)
        {
            try
            {
                if (id != productTier.ProductAdbId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveProductTier(productTier));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<CmmProduct>> Post(ProductTierDTO productTier)
        {
            return Ok(await _service.SaveProductTier(productTier));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProductTier(id));
        }
    }
}
