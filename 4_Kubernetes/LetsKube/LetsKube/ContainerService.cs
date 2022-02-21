namespace LetsKube
{
	using System;
	using System.Net.Http;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("[controller]")]
	public class ContainerService : ControllerBase
	{
		private static readonly Guid ServiceIdentity = Guid.NewGuid();

		[HttpGet("/")]
		public string GetHelloWorld() {
			return $"Hello World from {ServiceIdentity}\n";
		}

		[HttpGet("/container2")]
		public string GetHelloWorldFromAnotherContainer() {
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