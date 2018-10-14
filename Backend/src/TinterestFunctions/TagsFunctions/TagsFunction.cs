using System.Threading.Tasks;
using MetalsTeam.Tinterest.DataAccess;
using MetalsTeam.Tinterest.Infrastructure.Logging;
using MetalsTeam.Tinterest.TagsFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;

using MsLogger = Microsoft.Extensions.Logging.ILogger;

namespace MetalsTeam.Tinterest.TagsFunction
{
	public static class TagsFunction
	{
		[FunctionName("TagsFunction")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req, MsLogger log, ExecutionContext context)
		{
			return await new TagsService()
				.ForRequest(req)
				.WithExecutionContext(context)
				.WithLogger(new Logger(log))
				.WithDataAccessFacade(config => new SqlDataAccessFacade(config.GetConnectionString("sqlDbConnectionString")))
				.TryRun();
		}
	}
}
