using AutoMapper;
using MyBlog.Model;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.AutoMapper
{
    public class CustomAutoMapperProfile :Profile
    {
        public CustomAutoMapperProfile() 
        {
            base.CreateMap<WriterInfo, WriterDTO>();
            base.CreateMap<BlogNews, BlogNewsDTO>()
                .ForMember(dest => dest.TypeName, source => source.MapFrom(src => src.TypeInfo.Name))
                .ForMember(dest => dest.WriterName, source => source.MapFrom(src => src.WriterInfo.Name));
        }
    }
}
