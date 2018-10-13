using System.Threading.Tasks;
using MetalsTeam.Tinterest.Models;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalsTeam.Tinterest.TagsFunctions.Services
{
	public class TagsService : ServiceBase<Tag>
	{
		public override async Task<IActionResult> Run()
		{
			return this.request.Method == HttpMethods.Get ?
				(IActionResult) new OkObjectResult(await this.repository.GetAllAsync()) :
				new NotFoundResult();
		}
	}
}
