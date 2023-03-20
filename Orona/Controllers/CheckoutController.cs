using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orona.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CheckoutController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderHeader([FromBody] OrderHeaderCreateDto headerDto)
        {
            if (headerDto == null)
            {
                return BadRequest("Object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            var orderHeader = _mapper.Map<OrderHeader>(headerDto);

            orderHeader.Id = new Guid();
            orderHeader.OrderDate = DateTime.Now;
            orderHeader.OrderStatus = "New";
            orderHeader.PaymentStatus = "Not Paid";
            await _unitOfWork.OrderHeader.AddAsync(orderHeader);
            await _unitOfWork.SaveAsync();
            return Ok(orderHeader.Id);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetailCreateDto detailDto)
        {
            if (detailDto == null)
            {
                return BadRequest("Object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            var orderdetail = _mapper.Map<OrderDetail>(detailDto);

            await _unitOfWork.OrderDetail.AddAsync(orderdetail);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        
    }
}
