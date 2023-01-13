using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Orona.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class CartItemsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CartItemsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemsByUserName(string username)
        {
            IEnumerable<CartItem> cartItems = await _unitOfWork.CartItem.GetAllAsync(x => x.UserName == username);
            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCartItem([FromBody] CartItemCreateDto cartItemCreateDto)
        {
            if(cartItemCreateDto == null)
            {
                if (cartItemCreateDto == null || !ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);

                    await _unitOfWork.CartItem.AddAsync(cartItem);
                    await _unitOfWork.SaveAsync();
                    return Ok();
                }
            }
            else
            {
                if (cartItemCreateDto != null && ModelState.IsValid)
                {
                    var cartItem = await _unitOfWork.CartItem.GetCartItemByUsernameAndProductId(cartItemCreateDto.UserName, cartItemCreateDto.ProductId);
                    _mapper.Map(cartItemCreateDto, cartItem);
                    await _unitOfWork.CartItem.UpdateAsync(cartItem);
                    await _unitOfWork.SaveAsync();
                    return Ok();
                }

                return BadRequest();
            }
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
