using System;
using Management.Infrastructure.MessagingInfrastructure.Registry;
namespace Management.API.Registry
{
	public class WebRegistry : StructureMap.Registry
    {
        public WebRegistry()
        {
			IncludeRegistry<MessagingInfrastructureRegistry>();         
        }
    }
}
