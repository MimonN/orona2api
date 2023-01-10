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
        }
    }
}