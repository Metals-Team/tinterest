using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetalsTeam.Tinterest.Infrastructure.Logging;
using MetalsTeam.Tinterest.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MetalsTeam.Tinterest.UnitTests
{
	public class ServiceBaseTests
	{
		[Fact]
		public void ShouldLogServiceStartAndEndInfo_WhenExecutedWithoutErrors()
		{
			//Given
			List<string> loggerMessages = new List<string>();
			List<Exception> loggerExceptions = new List<Exception>();
			ILogger logger = GivenLogger(loggerMessages, loggerExceptions);
			ServiceBase<object> serviceBase = GivenServiceBaseImplementation(Task.FromResult(null as IActionResult));
			Action<string> logMessageValidator = (s => Assert.NotEqual(string.Empty, s));

			//When
			serviceBase
				.WithLogger(logger)
				.TryRun();

			//Then
			Assert.Empty(loggerExceptions);
			Assert.Collection(loggerMessages, logMessageValidator, logMessageValidator);
		}
		[Fact]
		public void ShouldLogServiceStartExceptionAndEndInfo_WhenExecutedWithErrors()
		{
			//Given
			List<string> loggerMessages = new List<string>();
			List<Exception> loggerExceptions = new List<Exception>();
			ILogger logger = GivenLogger(loggerMessages, loggerExceptions);
			ServiceBase<object> serviceBase = GivenServiceBaseImplementation(Task.FromException(new Exception()) as Task<IActionResult>);
			Action<string> logMessageValidator = (s => Assert.NotEqual(string.Empty, s));

			//When
			serviceBase
				.WithLogger(logger)
				.TryRun();

			//Then
			Assert.Single(loggerExceptions);
			Assert.Collection(loggerMessages, logMessageValidator, logMessageValidator, logMessageValidator);
		}

		private ServiceBase<object> GivenServiceBaseImplementation(Task<IActionResult> runImplementation)
		{
			Mock<ServiceBase<object>> serviceBaseImplementationMock = new Mock<ServiceBase<object>>();

			serviceBaseImplementationMock
				.Setup(p => p.Run())
				.Returns(runImplementation);

			serviceBaseImplementationMock
				.Setup(p => p.TryRun())
				.CallBase();

			return serviceBaseImplementationMock.Object;
		}

		private ILogger GivenLogger(List<string> loggerMessages, List<Exception> loggerExceptions)
		{
			Mock<ILogger> loggerMock = new Mock<ILogger>();

			loggerMock
				.Setup(p => p.Log(It.IsAny<string>()))
				.Callback<string>(text => loggerMessages.Add(text));

			loggerMock
				.Setup(p => p.LogError(It.IsAny<Exception>(), It.IsAny<string>()))
				.Callback<Exception, string>((exception, text) =>
					{
						loggerMessages.Add(text);
						loggerExceptions.Add(exception);
					});

			return loggerMock.Object;
		}
	}
}
