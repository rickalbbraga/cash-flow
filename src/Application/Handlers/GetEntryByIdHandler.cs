using Application.Queries;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Notification;
using Domain.Interfaces.Repositories;
using Domain.Utils;

namespace Application.Handlers
{
    public class GetEntryByIdHandler(
        IEntryRepository entryRepository,
        IMapper mapper,
        INotificationContext notificationContext
    ) : IQueryHandlerWithTResult<GetEntryByIdQuery, EntryResponse>
    {
        private readonly IEntryRepository _entryRepository = entryRepository;
        private readonly IMapper _mapper = mapper;
        private readonly INotificationContext _notificationContext = notificationContext;

        public async Task<EntryResponse?> HandleAsync(GetEntryByIdQuery query)
        {
            if (QueryIsNotValid(query)) return null;

            var result = await _entryRepository.GetByIdAsync(query.id);

            if (result is null) return null;

            return _mapper.Map<Entry, EntryResponse>(result);
        }

        private bool QueryIsNotValid(GetEntryByIdQuery query)
        {
            if (query.id == Guid.Empty)
            {
                _notificationContext.AddNotification(
                        DomainErrorMessage.BadRequestErrorCode,
                        DomainErrorMessage.TitleErrorMessage,
                        DomainErrorMessage.InvalidId);
                
                return true;
            }

            return false;
        }
    }
}