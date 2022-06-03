namespace LetsKube
{
	using System;
	using System.Net.Http;
	using Microsoft.AspNetCore.Mvc;
    using NLog;

    [ApiController]
	[Route("[controller]")]
	public class ContainerService : ControllerBase
	{
		private static readonly Guid ServiceIdentity = Guid.NewGuid();
		private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

		[HttpGet("/")]
		public string GetHelloWorld() {
			var helloMessage = $"Hello World from {ServiceIdentity}\n";
			_logger.Info(helloMessage);
			return helloMessage;
		}

		[HttpGet("/container2")]
		public string GetHelloWorldFromAnotherContainer() {
			_logger.Info($"Hello World from {ServiceIdentity}\n");
			var result = $"First hello from container {ServiceIdentity}\n";

			var anotherServiceUrl = Environment.GetEnvironmentVariable("AnotherServiceUrl");
			var client = new HttpClient();
			var response = client.GetStringAsync($"http://{anotherServiceUrl}/").Result;
			result += response;

			result += $"Second hello from container {ServiceIdentity}\n";
			return result;
		}

	}
}