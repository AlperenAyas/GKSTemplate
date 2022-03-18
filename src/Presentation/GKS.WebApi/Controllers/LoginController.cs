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
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpPost]
        [AllowAnonymous]
        [ApiVersion("2.0")]
        public async Task<IActionResult> Login(LoginQueryRequest request)
        {
            var response = await _mediator.Send(request);
            if(response.Token !=null)
                return Ok(response);
            return BadRequest("Username or password cannot match !");
        }
    }
}
