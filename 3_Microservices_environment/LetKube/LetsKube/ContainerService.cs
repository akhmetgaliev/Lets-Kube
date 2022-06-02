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
			_logger.Info("LETSKUBE. Get API(/) called");
			return $"Hello World from {ServiceIdentity}\n";
		}

		[HttpGet("/container2")]
		public string GetHelloWorldFromAnotherContainer() {
			_logger.Info("LETSKUBE. Get API(/container2) called");
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