using System;
using Microsoft.Extensions.Logging;
using MsLogger = Microsoft.Extensions.Logging.ILogger;

namespace MetalsTeam.Tinterest.Infrastructure.Logging
{
	public class Logger : ILogger
	{
		private readonly MsLogger log;

		public Logger(MsLogger log)
		{
			this.log = log;
		}

		public void Log(string message) => this.log.LogInformation(message);
		public void LogError(Exception exception, string message) => this.log.LogError(exception, message);
	}
}
