namespace Domain.Interfaces.CQS
{
    public interface IQueryHandlerWithTResult<in TQuery, TResult> where TQuery : IQuery where TResult : IResult
    {
        Task<TResult?> HandleAsync(TQuery query);        
    }
}