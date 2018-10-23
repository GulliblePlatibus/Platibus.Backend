using System;
using SimpleSoft.Mediator;
namespace Management.Queries.Registry
{
	public class QueryRegistry : StructureMap.Registry
    {
        public QueryRegistry()
        {
			Scan(scanner =>
			{
				//Define that this scanning goes for the assembly or project that contains the CommandRegistry class
				scanner.AssemblyContainingType<QueryRegistry>();

                //Scanning conventions, instead of defining each new handler in the this component we tell structuremap to look for all class implementing respective interfaces
				scanner.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>));
			});         
        }
    }
}
