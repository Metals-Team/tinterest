using MetalsTeam.Tinterest.UserFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MetalsTeam.Tinterest.UserFunctions
{
	public static class UserConfigurationFunction
	{
		[FunctionName("UserConfigurationFunction")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
		{
			return await new UserConfigurationService()
				.ForRequest(req)
				.WithExecutionContext(context)
				.WithLogger(log)
				.Run();
		}
	}
}
