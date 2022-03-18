using AutoMapper;
using GKS.Application.Abstrations.Services;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using GKS.Application.Features.CQRS.Response.CommandResponse.HotelCommandResponses;
using GKS.Application.Wrappers;
using GKS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Handlers.CommandHandlers.HotelCommandHandlers
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelRequest, TResult<DeleteHotelResponse>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public DeleteHotelCommandHandler(IUow uow, IMapper mapper, ICacheService cacheService)
        {
            _uow = uow;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<TResult<DeleteHotelResponse>> Handle(DeleteHotelRequest request, CancellationToken cancellationToken)
        {
            //Connected Update işlemleri için Cahnge Tracking'in mevcut entity'i izlemesi gerekir.
            //Bir başka yöntem olarak. QueryTrackingBehavior.NoTracking seçilirse Disconnected Update için Update metoduna dönüş yapan entity'i yollamak yeterlidir.

            var operationData =  await _uow.GetQueryRepository<Hotel>().GetByFilterAsync(x => x.Id == request.Id,null,Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll);

            operationData.Deleted = true;

            await _uow.SaveChangesAsync();

            await _cacheService.DeleteCacheValueLikeAsync("Hotelskj");

            return new TResult<DeleteHotelResponse>(null, "Veri silinmiştir", true, 100);
        }
    }
}
