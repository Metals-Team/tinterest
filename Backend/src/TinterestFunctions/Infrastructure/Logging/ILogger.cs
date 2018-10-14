using System;

namespace MetalsTeam.Tinterest.Infrastructure.Logging
{
	public interface ILogger
	{
		void Log(string message);
		void LogError(Exception ex, string message);
	}
}