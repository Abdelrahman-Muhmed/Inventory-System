using AutoMapper;
using Inventory_System.Dtos;
using Inventory_System_Core.Model;

namespace Inventory_System.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, ProductRetuenDto>()
               .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
               .ForMember(p => p.CategoryName, o => o.MapFrom(s => s.CategoryName.Name));

        }

    }
}
