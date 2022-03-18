using GKS.Application.Features.CQRS.Response.QueryResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Requests.QueryRequests
{
    public class LoginQueryRequest : IRequest<LoginQueryResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
