using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces.CQS;

namespace Application.Queries
{
    [ExcludeFromCodeCoverage]
    public record GetEntryByIdQuery : IQuery
    {
        public Guid id { get; set; }
    }
}