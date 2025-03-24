using System.Text.Json;
using Application.Commands;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Notification;
using Domain.Interfaces.Repositories;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class UpdateEntryHanlder(
        ILogger<UpdateEntryHanlder> logger,
        INotificationContext notificationContext,
        IMapper mapper,
        IEntryRepository entryRepository
    ) : ICommandHandlerWithTResult<UpdateEntryCommand, EntryResponse>
    {
        private readonly ILogger<UpdateEntryHanlder> _logger = logger;
        private readonly INotificationContext _notificationContext = notificationContext;
        private readonly IMapper _mapper = mapper;
        private readonly IEntryRepository _entryRepository = entryRepository;

        public async Task<EntryResponse?> HandleAsync(UpdateEntryCommand command)
        {
            _logger.LogInformation(
                $"Requisição recebida: Command = { JsonSerializer.Serialize(command) }");

            if (CommandIsNotValid(command)) return null;

            var entry = await _entryRepository.GetByIdAsync(command.Id);

            if (EntryIsNull(entry)) return null;
            
            entry!.Update(command.Date, command.Description, command.Value, command.Type);

            _notificationContext.AddNotifier(entry);

            if (_notificationContext.HasNotifications()) return null;

            await _entryRepository.UpdateAsync(entry);

            _logger.LogInformation(
                $"Lançamento atualizado com sucesso: Entry = { JsonSerializer.Serialize(entry) }");
            
            return _mapper.Map<Entry, EntryResponse>(entry);
        }

        private bool CommandIsNotValid(UpdateEntryCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                _notificationContext.AddNotification(
                        DomainErrorMessage.BadRequestErrorCode,
                        DomainErrorMessage.TitleErrorMessage,
                        DomainErrorMessage.InvalidId);
                
                return true;
            }

            return false;
        }

        private bool EntryIsNull(Entry? entry) 
        {
            if (entry is null)
            {
                _notificationContext.AddNotification(
                    DomainErrorMessage.BadRequestErrorCode,
                    DomainErrorMessage.TitleErrorMessage,
                    DomainErrorMessage.EntryNotFound);
                
                return true;
            }

            return false;
        }
    }
}