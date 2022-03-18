using AutoMapper;
using GKS.Application.Abstrations.Services;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using GKS.Application.Features.CQRS.Response.QueryResponse;
using GKS.Application.Wrappers;
using GKS.Domain.Entities;
using GKS.Domain.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Handlers.QueryHandlers
{
    public class GetHotelsForComboBoxQueryHandler : IRequestHandler<GetHotelsForComboBoxQueryRequest, TResult<List<GetHotelsForComboBoxQueryResponse>>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetHotelsForComboBoxQueryHandler(IUow uow, IMapper mapper, ICacheService cacheService)
        {
            _uow = uow;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<TResult<List<GetHotelsForComboBoxQueryResponse>>> Handle(GetHotelsForComboBoxQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _uow.GetQueryRepository<Hotel>().RawQueryAsync<HotelComboBoxQuery>("Select \"Id\",\"Name\" from public.\"Hotels\";");

            if (data == null)
                return new TResult<List<GetHotelsForComboBoxQueryResponse>>(null, "Veri Bulunamadı", false, 200);

            var mappedData = _mapper.Map <List<GetHotelsForComboBoxQueryResponse>>(data);

            return new TResult<List<GetHotelsForComboBoxQueryResponse>>(mappedData, "İşlem başarılı");
        }
    }
}
