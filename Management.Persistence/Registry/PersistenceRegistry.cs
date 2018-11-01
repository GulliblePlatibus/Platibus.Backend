using System;
using Management.Persistence.Model;
using Management.Persistence.Repositories;

namespace Management.Persistence.Registry
{
	public class PersistenceRegistry : StructureMap.Registry
    {
        public PersistenceRegistry()
        {
	       
	        
			Scan(scanner =>
			{
				//Define that this scanning goes for the assembly or project that contains the CommandRegistry class
				scanner.AssemblyContainingType<PersistenceRegistry>();

				//Scan this assembly/Management.Persistence for all single implementation of interface and include them in the registry
                //Directs the scanning to automatically register any type that is the single implementation of an interface.
				scanner.SingleImplementationsOfInterface();
			});         
        }
    }
}
