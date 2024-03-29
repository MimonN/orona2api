﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Orona
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();

            CreateMap<EstimateRequest, EstimateRequestDto>().ReverseMap();
            CreateMap<EstimateRequest, EstimateRequestUpdateDto>().ReverseMap();
            CreateMap<EstimateRequest, EstimateRequestCreateDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreateDto>().ReverseMap();

            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderCreateDto>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderUpdateDto>().ReverseMap();

            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<CartItemCreateDto, CartItem>().ReverseMap();
        }
    }
}
