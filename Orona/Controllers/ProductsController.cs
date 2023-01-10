using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orona.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProductsController(ApplicationDbContext db, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            return Ok(product); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if(productCreateDto == null)
            {
                return BadRequest("Product object is null");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            var product = _mapper.Map<Product>(productCreateDto);
            var ifProductExists = await _unitOfWork.Product.ProductExistAsync(product);
            if(ifProductExists == null)
            {
                await _unitOfWork.Product.AddAsync(product);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This product already exists");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if(productUpdateDto == null)
            {
                return BadRequest("Produc object is null");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                return NotFound($"Product with id: {id} has not been found in db.");
            }

            _mapper.Map(productUpdateDto, product);

            var ifProductExists = await _unitOfWork.Product.ProductExistAsync(product);
            if(ifProductExists == null)
            {
                await _unitOfWork.Product.UpdateAsync(product);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }

            return BadRequest("This product already exists");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            var oldImage = product.ImageUrl;
            if (System.IO.File.Exists(oldImage))
            {
                System.IO.File.Delete(oldImage);
            }

            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
