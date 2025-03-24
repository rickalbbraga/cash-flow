using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEntryRepository
    {
        Task<IList<Entry>> GetAllAsync();
        
        Task<Entry?> GetByIdAsync(Guid id);
        
        Task AddAsync(Entry entry);
        
        Task UpdateAsync(Entry entry);
        
        Task DeleteAsync(Entry entry);
    }
}