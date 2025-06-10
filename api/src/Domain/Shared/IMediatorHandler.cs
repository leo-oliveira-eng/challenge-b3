using MediatR;

namespace Domain.Shared;

public interface IMediatorHandler
{
    Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : IRequest<TResult>
        where TResult : class;
}
