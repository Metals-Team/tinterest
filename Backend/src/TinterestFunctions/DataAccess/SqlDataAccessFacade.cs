using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace MetalsTeam.Tinterest.DataAccess
{
	public class SqlDataAccessFacade : IDataAccessFacade
	{
		private readonly string connectionString;

		public SqlDataAccessFacade(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public Task<int> ExecuteAsync(string script, object parameters = null)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return db.ExecuteAsync(script, parameters);
			}
		}

		public Task<IEnumerable<TModel>> QueryAsync<TModel>(string script, object parameters = null)
		{
			using (IDbConnection db = new SqlConnection(this.connectionString))
			{
				return db.QueryAsync<TModel>(script, parameters);
			}
		}
	}
}
