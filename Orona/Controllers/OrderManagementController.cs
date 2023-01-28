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
    public class OrderManagementController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OrderManagementController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<OrderHeader> orders = await _unitOfWork.OrderHeader.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderHeaderById(Guid id)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetFirstOrDefaultAsync(x => x.Id == id);
            return Ok(orderHeader);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderDetailsById(Guid id)
        {
            var orderDetails = await _unitOfWork.OrderDetail.GetAllAsync(x => x.OrderHeaderId == id, includeProperties: "Product");
            return Ok(orderDetails); 
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateEstimate([FromBody] EstimateRequestCreateDto estimateCreateDto)
        //{
        //    if(estimateCreateDto == null)
        //    {
        //        return BadRequest("Estimate object is null");
        //    }

        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid Model Object");
        //    }

        //    var estimate = _mapper.Map<EstimateRequest>(estimateCreateDto);

        //    estimate.Status = "New";
        //    estimate.Created = DateTime.Now;


        //    await _unitOfWork.EstimateRequest.AddAsync(estimate);
        //    await _unitOfWork.SaveAsync();
        //    return NoContent();
        //}

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderHeaderUpdateDto orderUpdateDto)
        {
            if(orderUpdateDto == null)
            {
                return BadRequest("Object is null");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            
            var order = await _unitOfWork.OrderHeader.GetFirstOrDefaultAsync(x => x.Id == id);
            if(order == null)
            {
                return NotFound($"OrderHeader with id: {id} has not been found in db.");
            }

            _mapper.Map(orderUpdateDto, order);
            order.UpdateTime = DateTime.Now;

            await _unitOfWork.OrderHeader.UpdateAsync(order);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteEstimate(int id)
        //{
        //    var estimate = await _unitOfWork.EstimateRequest.GetFirstOrDefaultAsync(x => x.Id == id);
        //    if(estimate == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.EstimateRequest.Remove(estimate);
        //    await _unitOfWork.SaveAsync();

        //    return NoContent();
        //}
    }
}
