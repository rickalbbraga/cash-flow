using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly AppDbContext _dbContext;

        public EntryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Entry entry)
        {
            await _dbContext.Entries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entry entry)
        {
            _dbContext.Entries.Remove(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Entry>> GetAllAsync()
        {
            return await _dbContext.Entries.ToListAsync();
        }

        public async Task<Entry?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Entries.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Entry entry)
        {
            _dbContext.Entries.Update(entry);
            await _dbContext.SaveChangesAsync();
        }
    }
}