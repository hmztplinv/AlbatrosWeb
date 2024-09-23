public interface ISliderService
{
    Task<IEnumerable<SliderDto>> GetAllSlidersAsync();
    Task<SliderDto> GetSliderByIdAsync(int id);
    Task AddSliderAsync(CreateSliderDto sliderDto);
    Task UpdateSliderAsync(UpdateSliderDto sliderDto);
    Task DeleteSliderAsync(int id);
}