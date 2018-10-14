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

namespace UserFunctions
{
	public static class UsersInRangeFunction
	{
		[FunctionName("UsersInRangeFunction")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, MsLogger log, ExecutionContext context)
		{
			return await new UsersInRangeService()
				.ForRequest(req)
				.WithExecutionContext(context)
				.WithLogger(new Logger(log))
				.WithDataAccessFacade(config => new SqlDataAccessFacade(config.GetConnectionString("sqlDbConnectionString")))
				.TryRun();
		}
	}
}
