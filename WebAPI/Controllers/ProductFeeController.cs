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
    public class ProductFeeController : ControllerBase
    {
        private readonly IProductFeeService _service;
        public ProductFeeController(IProductFeeService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductFeeDTO>>> GetList(int ProductAdbId, int page = 1,string search = "")
        {
            return await _service.GetProductFeeList(ProductAdbId, page, search);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductFeeDTO productFee)
        {
            try
            {
                if (id != productFee.ProductAdbId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveProductFee(productFee));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<CmmProduct>> Post(ProductFeeDTO productFee)
        {
            return Ok(await _service.SaveProductFee(productFee));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProductFee(id));
        }
    }
}
