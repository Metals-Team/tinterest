using DataAccess.Repositories;
using MetalsTeam.Tinterest.Models;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MetalsTeam.Tinterest.TagsFunctions.Services
{
	public class TagsService : ServiceBase
	{
		public override async Task<IActionResult> Run()
		{
			var repository = new Repository<Tag>(this.configuration.GetConnectionString("sqlDbConnectionString"));

			if (this.request.Method == HttpMethods.Get)
			{
				return new OkObjectResult(await repository.GetAllAsync());
			}
			else
			{
				return await Task.FromResult(new NotFoundResult());
			}
		}
	}
}
