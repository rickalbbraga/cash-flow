using System.Text.Json;
using Application.Commands;
using Domain.Entities;
using Domain.Interfaces.CQS;
using Domain.Interfaces.Notification;
using Domain.Interfaces.Repositories;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class DeleteEntryHandler(
        ILogger<DeleteEntryHandler> logger,
        INotificationContext notificationContext,
        IEntryRepository entryRepository
    ) : ICommandHandlerWithoutTResult<DeleteEntryCommand>
    {
        private readonly ILogger<DeleteEntryHandler> _logger = logger;
        private readonly INotificationContext _notificationContext = notificationContext;
        private readonly IEntryRepository _entryRepository = entryRepository;

        public async Task HandleAsync(DeleteEntryCommand command)
        {
            _logger.LogInformation(
                $"Requisição recebida: Command = { JsonSerializer.Serialize(command) }");

            var entry = await _entryRepository.GetByIdAsync(command.Id);

            if (EntryIsNull(entry)) return;

            await _entryRepository.DeleteAsync(entry!);

            _logger.LogInformation(
                $"Lançamento deletado com sucesso: Id = { JsonSerializer.Serialize(command.Id) }");
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