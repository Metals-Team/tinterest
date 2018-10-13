using System.Security.Claims;
using System.Threading;

namespace MetalsTeam.Tinterest.Infrastructure.Authentication
{
	public class CurrentUser
	{
		//TODO: We should consider replacing FacebookId in DB with UserId and start using other identity providers
		public static string FacebookId => (Thread.CurrentPrincipal as ClaimsPrincipal).FindFirst("stable_sid").Value;
	}
}
