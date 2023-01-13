using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Orona.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CartController(ApplicationDbContext db, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemCreateDto cartItemCreateDto)
        {
            if (cartItemCreateDto == null)
            {
                return BadRequest("CartItemCreateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);

            await _unitOfWork.CartItem.AddAsync(cartItem);
            await _unitOfWork.SaveAsync();
            return NoContent();

            //var ifProductExists = await _unitOfWork.Product.ProductExistAsync(product);
            //if (ifProductExists == null)
            //{
            //    await _unitOfWork.Product.AddAsync(product);
            //    await _unitOfWork.SaveAsync();
            //    return NoContent();
            //}

            //return BadRequest("This product already exists");
        }
    }
}
