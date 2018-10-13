using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetalsTeam.Tinterest.DataAccess
{
	public interface IDataAccessFacade
	{
		Task<int> ExecuteAsync(string script, object parameters = null);
		Task<IEnumerable<TModel>> QueryAsync<TModel>(string script, object parameters = null);
	}
}
