using System;
using SimpleSoft.Mediator;
using Management.Infrastructure.MessagingInfrastructure.Factory;
using Management.Infrastructure.MessagingContracts;
using Management.Infrastructure.MessagingInfrastructure.Mediators;
using Management.Domain.Registry;
using Management.Persistence.Registry;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using Management.API.Helpers;
using Management.API.Registry;

namespace Management.UnitTest
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            IncludeRegistry<WebRegistry>();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile($"AppSettings.json", optional: true);

            var config = builder.Build();

            For<IOptions<IdentityServerConfiguration>>().Use(Options.Create(config
                .GetSection(nameof(IdentityServerConfiguration)).Get<IdentityServerConfiguration>()));

            
            

        }
    }
}
