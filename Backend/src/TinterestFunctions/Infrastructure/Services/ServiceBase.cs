using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MetalsTeam.Tinterest.Models.Services
{
	public abstract class ServiceBase
	{
		protected IConfigurationRoot configuration;
		protected ILogger logger;
		protected HttpRequest request;
		protected DocumentClient dbClient;

		public ServiceBase WithExecutionContext(ExecutionContext context)
		{
			this.configuration = new ConfigurationBuilder()
				.SetBasePath(context.FunctionAppDirectory)
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			return this;
		}

		public ServiceBase WithLogger(ILogger logger)
		{
			this.logger = logger;
			return this;
		}

		public ServiceBase ForRequest(HttpRequest request)
		{
			this.request = request;
			return this;
		}

		public ServiceBase WithDocumentDb()
		{
			this.dbClient = GetDbClient();
			return this;
		}

		public abstract Task<IActionResult> Run();

		private DocumentClient GetDbClient()
			=> new DocumentClient(new System.Uri(this.configuration.GetValue<string>("DbEndpoint")),
				 this.configuration.GetValue<string>("DbToken"));
	}
}
