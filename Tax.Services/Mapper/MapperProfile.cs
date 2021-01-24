using AutoMapper;
using Tax.Models.Models;
using Taxjar;

namespace Tax.Services.Mapper
{ 

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Tax.Models.Models.Order, Taxjar.Order>().ReverseMap();
        }
    }
   
}
