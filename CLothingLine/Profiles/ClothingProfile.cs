using AutoMapper;
using CLothingLine.Dtos;
using CLothingLine.Models;

namespace CLothingLine.Profiles
{
    public class ClothingProfile : Profile
    { 
        //source target
        public ClothingProfile() {

            CreateMap<Clothing, ReadClothsDto>();
            CreateMap<CreateClothDto, Clothing>();
            CreateMap<UpDateClothDto, Clothing>();
            CreateMap<Clothing, UpDateClothDto>();
        }
    }
}
