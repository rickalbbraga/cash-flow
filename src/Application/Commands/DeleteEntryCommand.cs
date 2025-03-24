using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Domain.Interfaces.CQS;

namespace Application.Commands
{
    [ExcludeFromCodeCoverage]
    public record DeleteEntryCommand : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}