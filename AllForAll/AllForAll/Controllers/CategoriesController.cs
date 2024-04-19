
using BusinessLogic.Interfaces;
using BusinessLogic.Dto.Category;
using Microsoft.AspNetCore.Mvc;


namespace AllForAll.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
       


        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryRequestDto category, CancellationToken cancellationToken)
        {
            var categoryId = await _categoryService.CreateCategoryAsync(category, cancellationToken);
            return Ok(categoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromBody] CategoryRequestDto category, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateCategoryAsync(id, category, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteCategoryAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
