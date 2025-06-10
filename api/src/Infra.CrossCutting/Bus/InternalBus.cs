using Domain.Shared;
using MediatR;
using System.Runtime.CompilerServices;

namespace Infra.CrossCutting.Bus;
public class InternalBus(IMediator mediator) : IMediatorHandler
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : IRequest<TResult>
        where TResult : class
        => await mediator.Send(command, cancellationToken);
}
