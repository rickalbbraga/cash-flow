namespace Domain.Interfaces.CQS
{
    public interface ICommandHandlerWithoutTResult<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}