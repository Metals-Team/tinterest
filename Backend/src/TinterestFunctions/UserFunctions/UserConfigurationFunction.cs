using System.Threading.Tasks;
using MetalsTeam.Tinterest.DataAccess;
using MetalsTeam.Tinterest.Infrastructure.Logging;
using MetalsTeam.Tinterest.UserFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;

using MsLogger = Microsoft.Extensions.Logging.ILogger;

namespace MetalsTeam.Tinterest.UserFunctions
{
	public static class UserConfigurationFunction
	{
		[FunctionName("UserConfigurationFunction")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = null)]HttpRequest req, MsLogger log, ExecutionContext context)
		{
			return await new UserConfigurationService()
				.ForRequest(req)
				.WithExecutionContext(context)
				.WithLogger(new Logger(log))
				.WithDataAccessFacade(config => new SqlDataAccessFacade(config.GetConnectionString("sqlDbConnectionString")))
				.TryRun();
		}
	}
}
