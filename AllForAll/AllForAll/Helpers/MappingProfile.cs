using AllForAll.Dto.Product;
using AllForAll.Models;
using AutoMapper;

namespace AllForAll.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<ProductRequestDto, Product>();

        }

        
    }
}
