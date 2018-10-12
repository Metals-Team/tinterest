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
	public class UsersInRangeService : ServiceBase
	{
		public override async Task<IActionResult> Run()
		{
			var repository = new Repository<UserConfiguration>(this.configuration.GetConnectionString("sqlDbConnectionString"));

			if (this.request.Method == HttpMethods.Get)
			{
				return new OkObjectResult(await repository.GetFilteredAsync(GetFilters(), "InRange"));
			}
			else
			{
				return new NotFoundResult();
			}
		}

		private async Task<RangeFilter> GetFilters()
		{
			using (var reader = new StreamReader(this.request.Body))
			{
				var filter =  JsonConvert.DeserializeObject<RadiusRangeFilter>(await reader.ReadToEndAsync());
				filter.CalculateRange();
				return filter;
			}
		}
	}
}
