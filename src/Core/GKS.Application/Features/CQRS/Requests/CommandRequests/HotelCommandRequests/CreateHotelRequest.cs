using GKS.Application.Features.CQRS.Response.CommandResponse;
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
    public class CreateHotelRequest : IRequest<TResult<CreateHotelResponse>>
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
    }
}
