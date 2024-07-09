using AutoMapper;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;

namespace InventoryWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();
        }
    }
}
