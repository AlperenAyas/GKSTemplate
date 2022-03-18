using AutoMapper;
using GKS.Application.Abstrations.Services;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Application.Features.CQRS.Requests.CommandRequests;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using GKS.Application.Features.CQRS.Response.CommandResponse;
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
    public class CrateHotelCommandHandler : IRequestHandler<CreateHotelRequest, TResult<CreateHotelResponse>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CrateHotelCommandHandler(IUow uow, IMapper mapper, ICacheService cacheService)
        {
            _uow = uow;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<TResult<CreateHotelResponse>> Handle(CreateHotelRequest request, CancellationToken cancellationToken)
        {
            var mappedRequest = _mapper.Map<Hotel>(request);

            var dataExist = await _uow.GetQueryRepository<Hotel>().GetByFilterAsync(x => x.Name == request.Name);

            
            if (dataExist != null)
            {
                var data = await _uow.GetCommandRepository<Hotel>().AddAsync(mappedRequest);

                await _uow.SaveChangesAsync();

                var result = await _cacheService.DeleteCacheValueLikeAsync("Hotels");

                var mappedData = _mapper.Map<CreateHotelResponse>(data);
                
                return new TResult<CreateHotelResponse>(mappedData, "Ekleme işlemi başarılı", true, 100);
            }
            else
            {
                return new TResult<CreateHotelResponse>(null, "Bu isimde bir kayıt bulunmaktadır", false, 200);
            }
        }
    }
}
