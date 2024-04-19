
using BusinessLogic.Dto.Product;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AllForAll.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync (CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] int id ,CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequestDto product , CancellationToken cancellationToken)
        {
            var productsId = await _productService.CreateProductAsync(product, cancellationToken); 
            return Ok(productsId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute]int id, [FromBody]ProductRequestDto product, CancellationToken cancellation = default)
        {
            await _productService.UpdateProductAsync(id, product, cancellation);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id, CancellationToken cancellation = default)
        {
            await _productService.DeleteProductAsync(id, cancellation);
            return NoContent();
        }


    }
}
