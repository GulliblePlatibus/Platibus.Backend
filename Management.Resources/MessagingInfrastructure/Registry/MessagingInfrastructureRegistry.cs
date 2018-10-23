using System;
using SimpleSoft.Mediator;
using Management.Infrastructure.MessagingInfrastructure.Factory;
using Management.Infrastructure.MessagingContracts;
using Management.Infrastructure.MessagingInfrastructure.Mediators;
using Management.Domain.Registry;
using Management.Queries.Registry;
using Management.Persistence.Registry;


namespace Management.Infrastructure.MessagingInfrastructure.Registry
{
	public class MessagingInfrastructureRegistry : StructureMap.Registry
    {
        public MessagingInfrastructureRegistry()
        {
			For<IMediator>().Use<Mediator>();//https://github.com/jbogard/MediatR/blob/master/src/MediatR/Mediator.cs
			For<IMediatorFactory>().Use<StructureMapMediatorFactory>();

			For<ICommandRouter>().Use<MediatorCommandRouter>();
			For<IQueryRouter>().Use<MediatorQueryRouter>();


			IncludeRegistry<CommandRegistry>();
			IncludeRegistry<QueryRegistry>();
			IncludeRegistry<PersistenceRegistry>();
        }
    }
}
