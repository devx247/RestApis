using AutoMapper;
using CqrsMediatR.Models;
using CqrsMediatR.ViewModels;

namespace CqrsMediatR
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
