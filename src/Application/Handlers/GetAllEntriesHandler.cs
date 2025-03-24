using Application.Queries;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers
{
    public class GetAllEntriesHandler(
        IEntryRepository entryRepository,
        IMapper mapper
    ) : IQueryHandlerWithTResultList<GetAllEntriesQuery, EntryResponse>
    {
        private readonly IEntryRepository _entryRepository = entryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<EntryResponse>?> HandleAsync(GetAllEntriesQuery query)
        {
            return _mapper.Map<IList<Entry>, IList<EntryResponse>>(await _entryRepository.GetAllAsync());
        }
    }
}