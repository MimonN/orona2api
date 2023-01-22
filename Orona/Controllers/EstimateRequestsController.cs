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
    public class EstimateRequestsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EstimateRequestsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEstimates()
        {
            IEnumerable<EstimateRequest> estimates = await _unitOfWork.EstimateRequest.GetAllAsync();
            return Ok(estimates);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEstimateById(int id)
        {
            var estimate = await _unitOfWork.EstimateRequest.GetFirstOrDefaultAsync(x => x.Id == id);
            return Ok(estimate); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstimate([FromBody] EstimateRequestCreateDto estimateCreateDto)
        {
            if(estimateCreateDto == null)
            {
                return BadRequest("Estimate object is null");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Model Object");
            }

            var estimate = _mapper.Map<EstimateRequest>(estimateCreateDto);

            estimate.Status = "New";
            estimate.Created = DateTime.Now;


            await _unitOfWork.EstimateRequest.AddAsync(estimate);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEstimate(int id, [FromBody] EstimateRequestUpdateDto estimateUpdateDto)
        {
            if(estimateUpdateDto == null)
            {
                return BadRequest("Estimate Request object is null");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            
            var estimate = await _unitOfWork.EstimateRequest.GetFirstOrDefaultAsync(x => x.Id == id);
            if(estimate == null)
            {
                return NotFound($"Estimate with id: {id} has not been found in db.");
            }

            _mapper.Map(estimateUpdateDto, estimate);
            estimate.Updated = DateTime.Now;

            await _unitOfWork.EstimateRequest.UpdateAsync(estimate);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEstimate(int id)
        {
            var estimate = await _unitOfWork.EstimateRequest.GetFirstOrDefaultAsync(x => x.Id == id);
            if(estimate == null)
            {
                return NotFound();
            }

            _unitOfWork.EstimateRequest.Remove(estimate);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
