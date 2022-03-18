using GKS.Application.Features.CQRS.Response.CommandResponse.HotelCommandResponses;
using GKS.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests
{
    public class DeleteHotelRequest : IRequest<TResult<DeleteHotelResponse>>
    {
        public Guid Id { get; set; }
    }
}
