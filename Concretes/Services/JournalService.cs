using AutoMapper;

public class JournalService : IJournalService
{
    private readonly IJournalRepository _journalRepository;
    private readonly IMapper _mapper;

    public JournalService(IJournalRepository journalRepository, IMapper mapper)
    {
        _journalRepository = journalRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JournalDto>> GetAllJournalsAsync()
    {
        var journals = await _journalRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<JournalDto>>(journals);
    }

    public async Task<JournalDto> GetJournalByIdAsync(int id)
    {
        var journal = await _journalRepository.GetByIdAsync(id);
        return _mapper.Map<JournalDto>(journal);
    }

    public async Task AddJournalAsync(JournalDto journalDto)
    {
        var journal = _mapper.Map<Journal>(journalDto);
        await _journalRepository.AddAsync(journal);
    }

    public async Task UpdateJournalAsync(int id, UpdateJournalDto updateJournalDto)
    {
        var journal = await _journalRepository.GetByIdAsync(id);
        _mapper.Map(updateJournalDto, journal);
        await _journalRepository.UpdateAsync(journal);
    }

    public async Task DeleteJournalAsync(int id)
    {
        var journal = await _journalRepository.GetByIdAsync(id);
        await _journalRepository.DeleteAsync(journal);
    }
}
