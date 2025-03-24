using System.Text.Json;
using Application.Commands;
using Application.Handlers;
using Application.Results;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Notification;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Domain.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Application.Handlers
{
    public class CreateEntryHandlerTest
    {
        private readonly Mock<ILogger<CreateEntryHandler>> logger;
        private readonly INotificationContext notificationContext;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IEntryRepository> entryRepository;
        private readonly Fixture fixture;
        private readonly CreateEntryHandler createEntryHandler;

        public CreateEntryHandlerTest()
        {
            logger = new Mock<ILogger<CreateEntryHandler>>();
            notificationContext = new NotificationContext();
            mapper = new Mock<IMapper>();
            entryRepository = new Mock<IEntryRepository>();
            fixture = new Fixture();

            createEntryHandler = new CreateEntryHandler(
                logger.Object,
                notificationContext,
                mapper.Object,
                entryRepository.Object
            );
        }
        
        [Fact(DisplayName = "HandleAsync - Deve retornar nulo quando o contexto de notificações tem erro.")]
        public async Task HandleAsync_ShouldReturnNullWhenNotificationContextHasNotification()
        {
            var command = fixture.Create<CreateEntryCommand>();
            command.Value = 0;
            var logMessage = $"Requisição recebida: Command = { JsonSerializer.Serialize(command) }";

            var result = await createEntryHandler.HandleAsync(command);

            result.Should().BeNull();
            notificationContext.HasNotifications().Should().BeTrue();
            notificationContext.Notifications.Any(n => n.Message == DomainErrorMessage.ValueErrorMessage).Should().BeTrue();
            logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains(logMessage)),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!
                ),
                Times.Once
            );
        }

        [Fact(DisplayName = "HandleAsync - Sucesso.")]
        public async Task HandleAsync_Success()
        {
            var command = fixture.Create<CreateEntryCommand>();
            command.Date = DateTime.Now;
            var entry = Entry.Create(DateTime.Now, "Unit Test", 10, EntryType.Debit);
            var entryResponse = new EntryResponse 
            { 
                Id = entry.Id,
                Date = entry.Date, 
                Description = entry.Description, 
                Value = entry.Value,
                Type = entry.Type 
            };
            entryRepository.Setup(_ => _.AddAsync(It.IsAny<Entry>())).Verifiable();
            mapper.Setup(_ => _.Map<Entry, EntryResponse>(It.IsAny<Entry>())).Returns(entryResponse);

            var result = await createEntryHandler.HandleAsync(command);

            result.Should().NotBeNull();
            result.Id.Should().Be(entry.Id);
            notificationContext.HasNotifications().Should().BeFalse();
            notificationContext.Notifications.Count().Should().Be(0);
            entryRepository.Verify(_ => _.AddAsync(It.IsAny<Entry>()), Times.Once());
            logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!
                ),
                Times.Exactly(2)
            );
        }
    }
}