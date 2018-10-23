using System;
using System.Collections.Generic;
using SimpleSoft.Mediator;
using SimpleSoft.Mediator.Pipeline;
using StructureMap;

namespace Management.Infrastructure.MessagingInfrastructure.Factory
{
	public class StructureMapMediatorFactory : IMediatorFactory
    {
		private readonly IContainer container;

		public StructureMapMediatorFactory(IContainer container)
        {
            this.container = container;
		}

		public ICommandHandler<TCommand> BuildCommandHandlerFor<TCommand>() where TCommand : ICommand
		{
			return container.GetInstance<ICommandHandler<TCommand>>();
		}

		public ICommandHandler<TCommand, TResult> BuildCommandHandlerFor<TCommand, TResult>() where TCommand : ICommand<TResult>
		{
			return container.GetInstance<ICommandHandler<TCommand, TResult>>();
		}

		public IEnumerable<ICommandMiddleware> BuildCommandMiddlewares()
		{
			return container.GetAllInstances<ICommandMiddleware>();
		}

		public IEnumerable<IEventHandler<TEvent>> BuildEventHandlersFor<TEvent>() where TEvent : IEvent
		{
			return container.GetAllInstances<IEventHandler<TEvent>>();
		}

		public IEnumerable<IEventMiddleware> BuildEventMiddlewares()
		{
			return container.GetAllInstances<IEventMiddleware>();
		}

		public IQueryHandler<TQuery, TResult> BuildQueryHandlerFor<TQuery, TResult>() where TQuery : IQuery<TResult>
		{
			return container.GetInstance<IQueryHandler<TQuery, TResult>>();
		}

		public IEnumerable<IQueryMiddleware> BuildQueryMiddlewares()
		{
			return container.GetAllInstances<IQueryMiddleware>();
		}
	}
}
