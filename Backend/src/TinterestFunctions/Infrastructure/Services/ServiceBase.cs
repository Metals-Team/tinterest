using System;
using System.IO;
using System.Threading.Tasks;
using MetalsTeam.Tinterest.DataAccess;
using MetalsTeam.Tinterest.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MetalsTeam.Tinterest.Models.Services
{
	public abstract class ServiceBase<TModel> where TModel : new()
	{
		protected IConfigurationRoot configuration;
		protected ILogger logger;
		protected HttpRequest request;
		protected DocumentClient dbClient;
		protected Repository<TModel> repository;

		public ServiceBase<TModel> WithExecutionContext(ExecutionContext context)
		{
			this.configuration = new ConfigurationBuilder()
				.SetBasePath(context.FunctionAppDirectory)
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.Build();

			return this;
		}

		public ServiceBase<TModel> WithLogger(ILogger logger)
		{
			this.logger = logger;
			return this;
		}

		public ServiceBase<TModel> ForRequest(HttpRequest request)
		{
			this.request = request;
			return this;
		}

		public ServiceBase<TModel> WithDocumentDb()
		{
			this.dbClient = GetDbClient();
			return this;
		}

		public ServiceBase<TModel> WithDataAccessFacade(Func<IConfigurationRoot, IDataAccessFacade> dataAccessFactory)
		{
			this.repository = new Repository<TModel>(dataAccessFactory(this.configuration));
			return this;
		}

		public abstract Task<IActionResult> Run();

		protected async Task<TFilterModel> GetRequestBodyAsync<TFilterModel>()
		{
			using (var reader = new StreamReader(this.request.Body))
			{
				var requestBody = JsonConvert.DeserializeObject<TFilterModel>(await reader.ReadToEndAsync());
				return requestBody;
			}
		}

		private DocumentClient GetDbClient()
			=> new DocumentClient(new System.Uri(this.configuration.GetValue<string>("DbEndpoint")),
				this.configuration.GetValue<string>("DbToken"));
	}
}
