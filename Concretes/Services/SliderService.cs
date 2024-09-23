public class SliderService : ISliderService
{
    private readonly IGenericRepository<Slider> _sliderRepository;

    public SliderService(IGenericRepository<Slider> sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }

    public async Task<IEnumerable<SliderDto>> GetAllSlidersAsync()
    {
        var sliders = await _sliderRepository.GetAllAsync();
        return sliders.Select(s => new SliderDto
        {
            Id = s.Id,
            Title = s.Title,
            ImageUrl = s.ImageUrl,
            Description = s.Description
        }).ToList();
    }

    public async Task<SliderDto> GetSliderByIdAsync(int id)
    {
        var slider = await _sliderRepository.GetByIdAsync(id);
        return new SliderDto
        {
            Id = slider.Id,
            Title = slider.Title,
            ImageUrl = slider.ImageUrl,
            Description = slider.Description
        };
    }

    public async Task AddSliderAsync(CreateSliderDto sliderDto)
    {
        var slider = new Slider
        {
            Title = sliderDto.Title,
            ImageUrl = sliderDto.ImageUrl,
            Description = sliderDto.Description
        };
        await _sliderRepository.AddAsync(slider);
    }

    public async Task UpdateSliderAsync(UpdateSliderDto sliderDto)
    {
        var slider = await _sliderRepository.GetByIdAsync(sliderDto.Id);
        if(slider != null)
        {
            slider.Title = sliderDto.Title;
            slider.ImageUrl = sliderDto.ImageUrl;
            slider.Description = sliderDto.Description;
            slider.UpdatedDate = DateTime.Now;

            await _sliderRepository.UpdateAsync(slider);
        }
    }

    public async Task DeleteSliderAsync(int id)
    {
        var slider = await _sliderRepository.GetByIdAsync(id);
        await _sliderRepository.DeleteAsync(slider);
    }
}