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
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQueryRequest, TResult<List<GetHotelsQueryResponse>>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ILogger<GetHotelsQueryHandler> _logger;

        public GetHotelsQueryHandler(IUow uow, IMapper mapper, ICacheService cacheService, ILogger<GetHotelsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<TResult<List<GetHotelsQueryResponse>>> Handle(GetHotelsQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = _cacheService.CreateCacheKey<GetHotelsQueryRequest>(request);

            var cachedValue = await _cacheService.GetCacheValueAsync<List<GetHotelsQueryResponse>>(cacheKey);

            if(cachedValue == null)
            {
                var data = await _uow.GetQueryRepository<Hotel>().GetAllByFilterAsync(x => x.Deleted == false);

                if (data == null)
                    return new TResult<List<GetHotelsQueryResponse>>(null,"Sonuç bulunamadı", false, 200);

                var mappedData = _mapper.Map<List<GetHotelsQueryResponse>>(data.ToList());

                await _cacheService.SetCacheValueAsync<List<GetHotelsQueryResponse>>(cacheKey, mappedData);

                _logger.LogInformation("Veriler Cache'e atıldı");

                return new TResult<List<GetHotelsQueryResponse>>(mappedData, "Veriler Veritabanından Getirildi", true, 100);
                
                
            }
            _logger.LogInformation("Veriler Cacheden getirildi");

            return new TResult<List<GetHotelsQueryResponse>>(cachedValue, "Veriler Cacheten Getirildi.", true, 101);
            

            
        }
    }
}
