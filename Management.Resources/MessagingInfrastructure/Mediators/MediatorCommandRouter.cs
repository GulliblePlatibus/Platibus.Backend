using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Infrastructure.MessagingContracts;
using SimpleSoft.Mediator;

namespace Management.Infrastructure.MessagingInfrastructure.Mediators
{
	public class MediatorCommandRouter : ICommandRouter
    {
		private readonly IMediator mediator;

		public MediatorCommandRouter(IMediator mediator)
        {
            this.mediator = mediator;
		}

		public async Task<TResponse> RouteAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand<TResponse>
		{
			return await mediator.SendAsync<TCommand, TResponse>(command, cancellationToken);
		}
	}
}
