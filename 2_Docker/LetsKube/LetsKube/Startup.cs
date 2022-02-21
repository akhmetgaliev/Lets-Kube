using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace LetsKube
{
	using System;
	using System.Net.Http;

	public class Startup
	{
		private static readonly Guid ServiceIdentity = Guid.NewGuid();

		public void ConfigureServices(IServiceCollection services) {
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
				endpoints.MapGet("/container2", async context => {
					await context.Response.WriteAsync($"First hello from container {ServiceIdentity}\n");

					var anotherServiceUrl = Environment.GetEnvironmentVariable("AnotherServiceUrl");
					var client = new HttpClient();
					var response = client.GetStringAsync($"http://{anotherServiceUrl}/").Result;
					await context.Response.WriteAsync(response);

					await context.Response.WriteAsync($"Second hello from container {ServiceIdentity}\n");
				});
			});
		}
	}
}
