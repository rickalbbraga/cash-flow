using Domain.Interfaces.CQS;

namespace Application.Queries
{
    public class GetEntriesReportQuery : IQuery
    {
        public DateTime Date { get; set; }
    }
}