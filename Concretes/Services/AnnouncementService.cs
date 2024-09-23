using AutoMapper;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IMapper _mapper;

    public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
    {
        _announcementRepository = announcementRepository;
        _mapper = mapper;
    }

    public async Task<List<AnnouncementDto>> GetAllAnnouncementsAsync()
    {
        var announcements = await _announcementRepository.GetAllAsync();
        return _mapper.Map<List<AnnouncementDto>>(announcements);
    }

    public async Task<AnnouncementDto> GetAnnouncementByIdAsync(int id)
    {
        var announcement = await _announcementRepository.GetByIdAsync(id);
        return _mapper.Map<AnnouncementDto>(announcement);
    }

    public async Task AddAnnouncementAsync(CreateAnnouncementDto createAnnouncementDto)
    {
        var announcement = _mapper.Map<Announcement>(createAnnouncementDto);
        await _announcementRepository.AddAsync(announcement);
    }

    public async Task UpdateAnnouncementAsync(UpdateAnnouncementDto updateAnnouncementDto)
    {
        var announcement = _mapper.Map<Announcement>(updateAnnouncementDto);
        await _announcementRepository.UpdateAsync(announcement);
    }

    public async Task DeleteAnnouncementAsync(int id)
    {
        var announcement = await _announcementRepository.GetByIdAsync(id);
        if (announcement != null)
        {
            await _announcementRepository.DeleteAsync(announcement);
        }
    }
}
