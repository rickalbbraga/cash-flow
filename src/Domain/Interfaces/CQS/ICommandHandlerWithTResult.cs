namespace Domain.Interfaces.CQS
{
    public interface ICommandHandlerWithTResult<in TCommand, TResult> where TCommand : ICommand where TResult : IResult
    {
        Task<TResult?> HandleAsync(TCommand command);
    }
}