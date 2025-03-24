namespace Domain.Interfaces.CQS
{
    public interface IQueryHandlerWithTResultList<in TQuery, TResult> where TQuery : IQuery where TResult : IResult
    {
        Task<IList<TResult>?> HandleAsync(TQuery query);  
    }
}