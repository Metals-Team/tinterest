
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UserFunctions
{
    public static class UsersInRangeFunction
    {
        [FunctionName("UsersInRangeFunction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
		{
			using (var reader = new StreamReader(this.request.Body))
			{
				return JsonConvert.DeserializeObject<RangeFilter>(await reader.ReadToEndAsync());			}

		}
    }
}
