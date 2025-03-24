using System.Text.Json;
using Application.Commands;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Notification;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class CreateEntryHandler(
        ILogger<CreateEntryHandler> logger,
        INotificationContext notificationContext,
        IMapper mapper,
        IEntryRepository entryRepository
    ) : ICommandHandlerWithTResult<CreateEntryCommand, EntryResponse>
    {
        private readonly ILogger<CreateEntryHandler> _logger = logger;
        private readonly INotificationContext _notificationContext = notificationContext;
        private readonly IMapper _mapper = mapper;
        private readonly IEntryRepository _entryRepository = entryRepository;

        public async Task<EntryResponse?> HandleAsync(CreateEntryCommand command)
        {
            _logger.LogInformation(
                $"Requisição recebida: Command = { JsonSerializer.Serialize(command) }");

            var entry = Entry.Create(command.Date, command.Description, command.Value, command.Type);

            _notificationContext.AddNotifier(entry);

            if (_notificationContext.HasNotifications()) return null;

            await _entryRepository.AddAsync(entry);

            _logger.LogInformation(
                $"Lançamento criado com sucesso: Entry = { JsonSerializer.Serialize(entry) }");
            
            return _mapper.Map<Entry, EntryResponse>(entry);
        }
    }
}