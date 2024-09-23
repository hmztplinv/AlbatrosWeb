using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Page, PageDto>().ReverseMap();
        CreateMap<CreatePageDto, Page>().ReverseMap();
        CreateMap<UpdatePageDto, Page>().ReverseMap();
        
        CreateMap<UpdatePageDto, PageDto>().ReverseMap();

        CreateMap<Slider, SliderDto>().ReverseMap();
        CreateMap<CreateSliderDto, Slider>().ReverseMap();
        CreateMap<UpdateSliderDto, Slider>().ReverseMap();

        CreateMap<Announcement, AnnouncementDto>().ReverseMap();
        CreateMap<CreateAnnouncementDto, Announcement>().ReverseMap();
        CreateMap<UpdateAnnouncementDto, Announcement>().ReverseMap();
        CreateMap<UpdateAnnouncementDto, AnnouncementDto>().ReverseMap();

    }
}
