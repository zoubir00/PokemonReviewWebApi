using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetCategories();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {   
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();
            var category = _categoryRepository.GetCategory(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }
        [HttpGet("Pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            var pokemons = _categoryRepository.GetPokemonByCategory(categoryId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
