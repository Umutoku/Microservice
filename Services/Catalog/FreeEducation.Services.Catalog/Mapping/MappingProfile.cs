using AutoMapper;

namespace FreeEducation.Services.Catalog.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Category, Dtos.CategoryDto>().ReverseMap();
            CreateMap<Models.Feature, Dtos.FeatureDto>().ReverseMap();
            CreateMap<Models.Education, Dtos.EducationDto>().ReverseMap();
            CreateMap<Models.Education, Dtos.EducationCreateDto>().ReverseMap();
            CreateMap<Models.Education, Dtos.EducationUpdateDto>().ReverseMap();

        }
    }
}
