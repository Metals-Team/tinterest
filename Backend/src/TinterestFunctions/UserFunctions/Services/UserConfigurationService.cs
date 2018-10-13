using System;
using System.Threading.Tasks;
using MetalsTeam.Tinterest.Models;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalsTeam.Tinterest.UserFunctions.Services
{
	public class UserConfigurationService : ServiceBase<UserConfiguration>
	{
		public override async Task<IActionResult> Run()
		{
			//TODO: Use CurrentUser.FacebookId to check if action is allowed
			if (this.request.Method == HttpMethods.Get && int.TryParse(this.request.Query["userId"], out int id))
			{
				return new OkObjectResult(await this.repository.GetAsync(id));
			}
			else if (this.request.Method == HttpMethods.Post)
			{
				return await ModifyUser(async userConfiguration => await this.repository.AddAsync(userConfiguration.Id, userConfiguration));
			}
			else if (this.request.Method == HttpMethods.Put)
			{
				return await ModifyUser(async userConfiguration => await this.repository.UpdateAsync(userConfiguration.Id, userConfiguration));
			}
			else if (this.request.Method == HttpMethods.Delete)
			{
				return await ModifyUser(async userConfiguration => await this.repository.DeleteAsync(userConfiguration.Id));
			}
			else
			{
				return new NotFoundResult();
			}
		}

		private async Task<IActionResult> ModifyUser(Func<UserConfiguration, Task> action)
		{
			await action(await GetRequestBodyAsync<UserConfiguration>());

			return new OkResult();
		}
	}
}
