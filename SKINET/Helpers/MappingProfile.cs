using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using SKINET.Dtos;

namespace SKINET.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(p => p.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<AppUser, UserDto>();
        }
    }
}
