using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Management.API.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using StructureMap;
using Management.API.Registry;
using Management.Persistence.Helpers;
using Management.Persistence.Model;

namespace Management.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the API before instantiation.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			//Configure ConfigurationFiles
			services.Configure<ElephantSQLConfiguration>(Configuration.GetSection(nameof(ElephantSQLConfiguration)));
			services.Configure<IdentityServerConfiguration>(
				Configuration.GetSection(nameof(IdentityServerConfiguration)));
			
			//API doesn't work without this is due to the fact that AddMVC adds both razor and json formatting that enabled the api to receive and transmit data smoothly --> https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/
			services.AddMvc();

			//Configure Platibus.Backend to use identityServer
			services.AddAuthentication("Bearer")
				.AddIdentityServerAuthentication(x =>
				{
					x.Authority = "https://localhost:5001";
					x.RequireHttpsMetadata = false;
					x.ApiName = "Platibus.Backend";
				});
			services.AddAuthorization();

			services.AddCors();
            //For fun swagger settings...
			services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("Management.API Auth", new OAuth2Scheme
				{
					Type = "oauth2",
					Flow = "application",
					Description = "This API uses the Management.API login Oauth2 Client Credentials flow",
					TokenUrl = "https://qa-auth-management-identity.azurewebsite.net/connect/token",
					Scopes = new Dictionary<string, string> { { "scope.fullacces", "Acces to all api-endpoints" } }
				});
				c.SwaggerDoc("v1", new Info { Title = "Management Backend", Version = "v1", Description = "Management API for use with prior agreement" });
			});

			//The api comes with standard dependency-injection, which is not that complicated nor have much functionality.
			//So we can re-configure the IOC container with our StructureMapRegistry.
			var container = new Container(new WebRegistry());
            

			//Settings for Dapper fluentmap 
			// Multiple ID's
			// https://github.com/henkmollema/Dommel
			FluentMapper.Initialize(options =>
			{
				options.AddMap(new WorkScheduleMap());
				options.AddMap(new UserMap());
				options.AddMap(new ShiftMap());
				options.ForDommel();
				
			});
			
			
			//The head of the container or dependency-injection tree has been set to the WebRegistry which conviniently includes, our MessaginRegistry and the tree will be build until there are no further registries to include.
			//Start configuration by using structuremap configure API
			container.Configure(config =>
			{
				//Start registering stuff in container, stuff is defined from the included registries. The stuff is registered at our service, so they are at our disposal.
				config.Populate(services);
			});

			//Assert validation. This tries a full test, to see if all the configuration are truely valid, and can be configured
			container.AssertConfigurationIsValid();

			// We need to change the method return type from void to IServiceProvider. This makes
            // ASP.NET use the StructureMap container to resolve its dependencies.
            return container.GetInstance<IServiceProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			//Enabled API to deliver swagger UI on http://{serverUrl}/swagger;
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
			});


            //Pre-configs from template
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

            //Pre-configs from template
			app.UseHttpsRedirection();

			app.UseAuthentication();
			
			app.UseMvc();
		}
	}
}
