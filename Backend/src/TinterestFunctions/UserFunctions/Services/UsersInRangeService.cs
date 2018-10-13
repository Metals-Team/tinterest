using System.Threading.Tasks;
using MetalsTeam.Tinterest.Models;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalsTeam.Tinterest.UserFunctions.Services
{
	public class UsersInRangeService : ServiceBase<UserConfiguration>
	{
		public override async Task<IActionResult> Run()
		{
			if (this.request.Method == HttpMethods.Post)
			{
				return new OkObjectResult(await this.repository.GetFilteredAsync(GetFilters(), "InRange"));
			}
			else
			{
				return new NotFoundResult();
			}
		}

		private async Task<RadiusRangeFilter> GetFilters()
		{
			var filter = await GetRequestBodyAsync<RadiusRangeFilter>();
			filter.CalculateRange();
			return filter;
		}
	}
}
