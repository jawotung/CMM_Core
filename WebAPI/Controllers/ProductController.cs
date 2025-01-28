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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDTO>>> GetList(int page = 1,string search = "")
        {
            return await _service.GetProductList(page, search);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDTO product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest();
                }
                return Ok(await _service.SaveProduct(product));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<CmmProduct>> Post(ProductDTO product)
        {
            return Ok(await _service.SaveProduct(product));
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteProduct(id));
        }
    }
}
