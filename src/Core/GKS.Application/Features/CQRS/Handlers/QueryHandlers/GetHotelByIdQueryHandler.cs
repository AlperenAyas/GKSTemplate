using AutoMapper;
using GKS.Application.Abstrations.Services;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using GKS.Application.Features.CQRS.Response.QueryResponse;
using GKS.Application.Wrappers;
using GKS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQueryRequest, TResult<GetHotelByIdQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly ICacheService _cacheService;
        private readonly ILogger<GetHotelByIdQueryHandler> _logger;

        public GetHotelByIdQueryHandler(IMapper mapper, IUow uow, ICacheService cacheService, ILogger<GetHotelByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<TResult<GetHotelByIdQueryResponse>> Handle(GetHotelByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = _cacheService.CreateCacheKey<GetHotelByIdQueryRequest>(request);

            var cacheData = await _cacheService.GetCacheValueAsync<GetHotelByIdQueryResponse>(cacheKey);

            if (cacheData != null)
                return new TResult<GetHotelByIdQueryResponse>(cacheData, "Veri cacheden getirildi",true,101);

            var data = await _uow.GetQueryRepository<Hotel>().GetByFilterAsync(x => x.Id == request.Id);

            if (data == null)
                return new TResult<GetHotelByIdQueryResponse>(null, "Kayıt Bulunamadı", false, 200);

            var mappedData = _mapper.Map<GetHotelByIdQueryResponse>(data);

            if(await _cacheService.SetCacheValueAsync<GetHotelByIdQueryResponse>(cacheKey, mappedData));
                _logger.LogInformation("Veri Cache yazıldı");

            return new TResult<GetHotelByIdQueryResponse>(mappedData, "Kayıt Databaseden getirildi");
            
        }
    }
}
