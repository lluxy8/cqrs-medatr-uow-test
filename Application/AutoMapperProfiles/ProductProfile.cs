using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateDto, ProductEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<ProductEntity, ProductViewModel>();

            CreateMap<ProductEntity, ProductViewModelWithUser>();

            CreateMap<ProductViewModel, ProductEntity>();
        }
    }
}
