using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces.CQS;

namespace Application.Queries
{
    [ExcludeFromCodeCoverage]
    public record GetAllEntriesQuery : IQuery
    {
        public int pageNumber { get; set; }

        public int pageSize { get; set; } = 10;
    }
}