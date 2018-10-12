using DataAccess.Repositories;
using MetalsTeam.Tinterest.Models;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MetalsTeam.Tinterest.UserFunctions.Services
{
	public class UserConfigurationService : ServiceBase
	{
		public override async Task<IActionResult> Run()
		{
			var repository = new Repository<UserConfiguration>(this.configuration.GetConnectionString("sqlDbConnectionString"));

			if (this.request.Method == HttpMethods.Get && int.TryParse(this.request.Query["userId"], out int id))
			{
				return new OkObjectResult(await repository.GetAsync(id));
			}
			else if (this.request.Method == HttpMethods.Post)
			{
				var userConfiguration = await GetUserConfigurationAsync();
				await repository.AddAsync(userConfiguration.Id, userConfiguration);

				return new OkResult();
			}
			else if (this.request.Method == HttpMethods.Put)
			{
				var userConfiguration = await GetUserConfigurationAsync();
				await repository.UpdateAsync(userConfiguration.Id, userConfiguration);

				return new OkResult();
			}
			else if (this.request.Method == HttpMethods.Delete)
			{
				var userConfiguration = await GetUserConfigurationAsync();
				await repository.DeleteAsync(userConfiguration.Id);

				return new OkResult();
			}
			else
			{
				return new NotFoundResult();
			}
		}

		private async Task<UserConfiguration> GetUserConfigurationAsync()
		{
			using (var reader = new StreamReader(this.request.Body))
			{
				return JsonConvert.DeserializeObject<UserConfiguration>(await reader.ReadToEndAsync());
			}
		}
	}
}
