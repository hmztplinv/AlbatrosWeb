public interface IAnnouncementService
{
    Task<List<AnnouncementDto>> GetAllAnnouncementsAsync();
    Task<AnnouncementDto> GetAnnouncementByIdAsync(int id);
    Task AddAnnouncementAsync(CreateAnnouncementDto createAnnouncementDto);
    Task UpdateAnnouncementAsync(UpdateAnnouncementDto updateAnnouncementDto);
    Task DeleteAnnouncementAsync(int id);
}
