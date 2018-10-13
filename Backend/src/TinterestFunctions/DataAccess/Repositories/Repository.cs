using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Properties;

namespace MetalsTeam.Tinterest.DataAccess.Repositories
{
	public class Repository<TModel> where TModel : new()
	{
		private readonly IDataAccessFacade dataAccess;

		public Repository(IDataAccessFacade dataAccess)
			=> this.dataAccess = dataAccess;

		public async Task<IEnumerable<TModel>> GetAllAsync()
			=> await this.dataAccess.QueryAsync<TModel>(GetScript("GetAll"));

		public async Task<TModel> GetAsync(int id)
			=> (await this.dataAccess.QueryAsync<TModel>(GetScript("Get"), new { Id = id })).Single();

		public async Task<IEnumerable<TModel>> GetFilteredAsync(object filters, string sufix)
			=> await this.dataAccess.QueryAsync<TModel>(GetScript($"GetFiltered{sufix}"), filters);

		public async Task<int> AddAsync(int id, TModel model)
			=> await this.dataAccess.ExecuteAsync(GetScript("Insert"), model);

		public async Task<int> UpdateAsync(int id, TModel model)
			=> await this.dataAccess.ExecuteAsync(GetScript("Update"), model);

		public async Task<int> DeleteAsync(int id)
			=> await this.dataAccess.ExecuteAsync(GetScript("Delete"), new { Id = id });

		private string GetScript(string type)
		{
			return typeof(Resources)
				.GetProperty($"{type}{GetType().GenericTypeArguments[0].Name}Query")
				.GetValue(null).ToString();
		}
	}
}
