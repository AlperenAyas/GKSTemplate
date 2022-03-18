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
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelRequest, TResult<UpdateHotelResponse>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UpdateHotelCommandHandler(IUow uow, IMapper mapper, ICacheService cacheService)
        {
            _uow = uow;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<TResult<UpdateHotelResponse>> Handle(UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var mappedReqeust = _mapper.Map<Hotel>(request);

            var result = await _uow.GetCommandRepository<Hotel>().UpdateAsync(mappedReqeust);

            await _uow.SaveChangesAsync();

            if (result)
            {
                await _cacheService.DeleteCacheValueLikeAsync("Hotels");

                return new TResult<UpdateHotelResponse>(null, "Güncelleme işlemi başarılı", result, 100);
            }
            else
            {
                return new TResult<UpdateHotelResponse>(null, "Güncelleme işlemi başarılı", result, 100);
            }
        }
    }
}
