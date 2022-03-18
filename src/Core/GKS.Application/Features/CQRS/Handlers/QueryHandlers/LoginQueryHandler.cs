using GKS.Application.Abstrations.Services;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using GKS.Application.Features.CQRS.Response.QueryResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly IJwtService _jwtService;

        public LoginQueryHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            LoginQueryResponse response =new LoginQueryResponse();
            if (request.Password=="1" && request.Username == "admin")
            {
                response =await Task.Run(()=> new LoginQueryResponse() { Token = _jwtService.GenerateToken(request) });
                
            }
            return response;
        }
    }
}
