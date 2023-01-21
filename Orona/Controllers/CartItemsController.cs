using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Orona.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CartItemsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CartItemsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetCartItemsByUserName(string username)
        {
            IEnumerable<CartItem> cartItems = await _unitOfWork.CartItem.GetAllAsync(x => x.UserName == username, includeProperties: "Product");
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCartItem([FromBody] CartItemCreateDto cartItemDto)
        {
            if(cartItemDto != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var cartItemExists = await _unitOfWork.CartItem.GetCartItemByUsernameAndProductId(cartItemDto.UserName, cartItemDto.ProductId);
                    if(cartItemExists != null)
                    {
                        cartItemExists.Count += cartItemDto.Count;
                        await _unitOfWork.CartItem.UpdateAsync(cartItemExists);
                        await _unitOfWork.SaveAsync();
                        return Ok();
                    }
                    else
                    {
                        var cartItem = _mapper.Map<CartItem>(cartItemDto);
                        await _unitOfWork.CartItem.AddAsync(cartItem);
                        await _unitOfWork.SaveAsync();
                        return Ok();
                    }
                }
            }
            return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItems(List<CartItem> cartItemsList)
        {
            if(cartItemsList == null)
            {
                return BadRequest("Cart is empty");
            }
            foreach(var item in cartItemsList)
            {
                CartItem itemFromDb = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(x => x.Id == item.Id);
                if(itemFromDb != null)
                {
                    itemFromDb.Count = item.Count;
                    if(itemFromDb.Count == 0)
                    {
                        _unitOfWork.CartItem.Remove(itemFromDb);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        await _unitOfWork.CartItem.UpdateAsync(itemFromDb);
                        await _unitOfWork.SaveAsync();
                    }
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(x => x.Id == id);  
            if(cartItem == null)
            {
                return NotFound();   
            }

            _unitOfWork.CartItem.Remove(cartItem);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}

