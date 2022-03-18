using GKS.Application.Features.CQRS.Response.QueryResponse;
using GKS.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Requests.QueryRequests
{
    public class GetHotelByIdQueryRequest : IRequest<TResult<GetHotelByIdQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
