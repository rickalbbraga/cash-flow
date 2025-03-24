using Application.Queries;
using Application.Results;
using Domain.Enums;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Repositories;

namespace Application.Handlers
{
    public class GetEntryReportHandler(
        IEntryRepository entryRepository
    ) : IQueryHandlerWithTResult<GetEntriesReportQuery, EntryReportResponse>
    {
        private readonly IEntryRepository _entryRepository = entryRepository;

        public async Task<EntryReportResponse?> HandleAsync(GetEntriesReportQuery query)
        {
            var entries = await _entryRepository.GetAllAsync();
            var incomings = entries.Where(e => e.Date == query.Date && e.Type == EntryType.Credit).Select(e => e.Value).Sum();
            var outcomings = entries.Where(e => e.Date == query.Date && e.Type == EntryType.Debit).Select(e => e.Value).Sum();

            return BuildResponse(query.Date, incomings, outcomings);
        }

        private static EntryReportResponse BuildResponse(DateTime date, decimal incomings, decimal outcomings)
        {
            return new EntryReportResponse
            {
                Date = date,
                Incomings = incomings,
                Outcomings = outcomings,
                Balance = incomings - outcomings
            };
        }
    }
}