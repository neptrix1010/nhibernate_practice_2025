using Microsoft.AspNetCore.Mvc;
using nhibernate_2.Models;
using nhibernate_2.Repositories;

namespace nhibernate_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            try
            {
                if (product.CategoryId <= 0)
                {
                    ModelState.AddModelError("CategoryId", "A valid CategoryId is required");
                    return BadRequest(ModelState);
                }

                // Clear any Category validation errors since we only need CategoryId
                ModelState.Remove("Category");
                
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _productRepository.Save(product);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("CategoryId", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (product == null || id != product.Id)
                return BadRequest();

            try
            {
                var existingProduct = _productRepository.GetById(id);
                if (existingProduct == null)
                    return NotFound();

                if (product.CategoryId <= 0)
                {
                    ModelState.AddModelError("CategoryId", "A valid CategoryId is required");
                    return BadRequest(ModelState);
                }

                _productRepository.Update(product);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("CategoryId", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            _productRepository.Delete(id);
            return NoContent();
        }
    }
}
