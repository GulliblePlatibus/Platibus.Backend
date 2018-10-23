using System;
using System.Threading.Tasks;
using System.Threading;
using SimpleSoft.Mediator;
namespace Management.Infrastructure.MessagingContracts
{
    public interface ICommandRouter
    {
		Task<TResponse> RouteAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand<TResponse>;
    }
}
