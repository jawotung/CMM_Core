using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.DTOs.Area;
using Application.Models.DTOs.Product;
using AutoMapper;
using WebAPI;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AreaDTO, CmmArea>();
            CreateMap<CmmArea, AreaDTO>();
            CreateMap<CmmArea, CmmArea>();

            CreateMap<ProductDTO, CmmProduct>();
            CreateMap<CmmProduct, ProductDTO>();
            CreateMap<CmmProduct, CmmProduct>();
        }
    }
}