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

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            IEnumerable<OrderDetail> orderDetails = await _unitOfWork.OrderDetail.GetAllAsync(null, includeProperties: "OrderHeader,Product");
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var orderDetail = await _unitOfWork.OrderDetail.GetFirstOrDefaultAsync(x => x.Id == id);
            return Ok(orderDetail);
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

        //[HttpPut("{id}")]
        ////[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateOrderDetail(int id, [FromBody] OrderDetailsUpdateDto orderDetailUpdateDto)
        //{
        //    if(orderDetailUpdateDto == null)
        //    {
        //        return BadRequest("OrderDetail object is null");
        //    }
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid model object");
        //    }

        //    var orderDetail = await _unitOfWork.OrderDetails.GetFirstOrDefaultAsync(x => x.Id == id);
        //    if(orderDetail == null)
        //    {
        //        return NotFound($"OrderDetail with id: {id} has not been found in db.");
        //    }

        //    _mapper.Map(orderDetailUpdateDto, orderDetail);
        //    orderDetail.UpdateTime = DateTime.Now;

        //    await _unitOfWork.OrderDetails.UpdateAsync(orderDetail);
        //    await _unitOfWork.SaveAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        ////[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteOrderDetail(int id)
        //{
        //    var orderDetail = await _unitOfWork.OrderDetails.GetFirstOrDefaultAsync(x => x.Id == id);
        //    if(orderDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.OrderDetails.Remove(orderDetail);
        //    await _unitOfWork.SaveAsync();

        //    return NoContent();
        //}
    }
}
