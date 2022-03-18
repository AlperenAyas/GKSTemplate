using GKS.Application.Features.CQRS.Requests.CommandRequests;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GKS.WebApi.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/xml")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetHotelsQueryRequest());
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Consumes("application/xml")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetHotelByIdQueryRequest() { Id=id });
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("[action]")]
        [Consumes("application/xml")]
        public async Task<IActionResult> GetForComboBox()
        {
            var result = await _mediator.Send(new GetHotelsForComboBoxQueryRequest());
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        [Consumes("application/xml")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> Create(CreateHotelRequest request)
        {
            var result = await _mediator.Send(request);
            if(!result.Success)
                return BadRequest(result);
            return Created("",result);
        }
        [HttpPut]
        [Consumes("application/xml")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> Update(UpdateHotelRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.Success)
                return BadRequest("Operation Failed");
            return NoContent();
        }
        [HttpDelete]
        [Consumes("application/xml")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> Delete(UpdateHotelRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.Success)
                return BadRequest("Operation Failed");
            return NoContent();
        }
    }
}
