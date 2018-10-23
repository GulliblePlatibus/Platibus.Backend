using System;
using Microsoft.AspNetCore.Mvc;
using Management.Infrastructure.MessagingContracts;
namespace Management.API.Controllers
{
	public class BaseController : ControllerBase
    {
		protected readonly ICommandRouter CommandRouter;
		protected readonly IQueryRouter QueryRouter;

		public BaseController(ICommandRouter commandRouter, IQueryRouter queryRouter)
        {
            this.QueryRouter = queryRouter;
			this.CommandRouter = commandRouter;
		}
    }
}
