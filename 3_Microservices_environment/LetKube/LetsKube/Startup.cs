using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LetsKube
{
	using System;
	using Microsoft.OpenApi.Models;

	public class Startup
	{
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My Api", Version = "v1"}); });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseRouting();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
