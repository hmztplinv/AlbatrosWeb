public interface IJournalService
{
    Task<IEnumerable<JournalDto>> GetAllJournalsAsync();
    Task<JournalDto> GetJournalByIdAsync(int id);
    Task AddJournalAsync(JournalDto journalDto);
    Task UpdateJournalAsync(int id, UpdateJournalDto updateJournalDto);
    Task DeleteJournalAsync(int id);
}
