using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Page, PageDto>().ReverseMap();
        CreateMap<CreatePageDto, Page>().ReverseMap();
        CreateMap<UpdatePageDto, Page>().ReverseMap();
        
        CreateMap<UpdatePageDto, PageDto>().ReverseMap();
    }
}
