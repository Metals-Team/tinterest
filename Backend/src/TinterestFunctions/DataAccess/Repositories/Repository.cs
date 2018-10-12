using Dapper;
using DataAccess.Properties;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class Repository<TModel> where TModel : new()
	{
		private readonly string connectionString;

		public Repository(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public async Task<IEnumerable<TModel>> GetAllAsync()
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return await db.QueryAsync<TModel>(GetScript("GetAll"));
			}
		}

		public async Task<TModel> GetAsync(int id)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return (await db.QueryAsync<TModel>(GetScript("Get"), new { Id = id })).Single();
			}
		}

		public async Task<IEnumerable<TModel>> GetFilteredAsync(object filters, string sufix)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return await db.QueryAsync<TModel>(GetScript($"GetFiltered{sufix}"), filters);
			}
		}

		public async Task<int> AddAsync(int id, TModel model)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return await db.ExecuteAsync(GetScript("Insert"), model);
			}
		}

		public async Task<int> UpdateAsync(int id, TModel model)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return await db.ExecuteAsync(GetScript("Update"), model);
			}
		}

		public async Task<int> DeleteAsync(int id)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return await db.ExecuteAsync(GetScript("Delete"), new { Id = id });
			}
		}

		private string GetScript(string type)
		{
			return typeof(Resources)
				.GetProperty($"{type}{GetType().GenericTypeArguments[0].Name}Query")
				.GetValue(null).ToString();
		}
	}
}
