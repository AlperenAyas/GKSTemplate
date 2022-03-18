using AutoMapper;
using GKS.Application.Features.CQRS.Requests.CommandRequests;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using GKS.Application.Features.CQRS.Requests.QueryRequests;
using GKS.Application.Features.CQRS.Response.CommandResponse;
using GKS.Application.Features.CQRS.Response.CommandResponse.HotelCommandResponses;
using GKS.Application.Features.CQRS.Response.QueryResponse;
using GKS.Domain.Entities;
using GKS.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Mappings
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            #region Queries Mapping
            this.CreateMap<Hotel, GetHotelsQueryResponse>().ReverseMap();

            this.CreateMap<Hotel, GetHotelByIdQueryRequest>().ReverseMap();
            this.CreateMap<Hotel, GetHotelByIdQueryResponse>().ReverseMap();

            this.CreateMap<GetHotelsForComboBoxQueryResponse, HotelComboBoxQuery>().ReverseMap();

            #endregion

            #region Commands Mapping

            this.CreateMap<Hotel, CreateHotelRequest>().ReverseMap();
            this.CreateMap<Hotel, CreateHotelResponse>().ReverseMap();

            this.CreateMap<Hotel, UpdateHotelRequest>().ReverseMap();
            this.CreateMap<Hotel, UpdateHotelResponse>().ReverseMap();

            this.CreateMap<Hotel, DeleteHotelRequest>().ReverseMap();
            this.CreateMap<Hotel, DeleteHotelResponse>().ReverseMap();

            #endregion
        }
    }
}
