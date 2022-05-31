using AudacesTestApi.DataComm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudacesTestApi {
	public class Startup {

		public Startup( IConfiguration configuration ) {
			Configuration = configuration;
		}




		public IConfiguration Configuration { get; }
		public string ConnectionString => Configuration["DbConnection:SqliteConnectionString"];




		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services ) {
			services.AddControllers();
			services.AddDbContext<MyDbContext>(options => options.UseSqlite(ConnectionString));
			services.AddMvc();
		}




		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
			if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
			app.UseRouting();
			app.UseAuthorization();
			app.Use(OutputRequestedEndpointToConsole);
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
			});
		}




		private Func<RequestDelegate,RequestDelegate> OutputRequestedEndpointToConsole = next => context => {
			var endpoint = context.GetEndpoint();
			if (endpoint is null) {
				return Task.CompletedTask;
			}
			Console.WriteLine($"Endpoint: {endpoint.DisplayName}");
			if (endpoint is RouteEndpoint routeEndpoint) {
				Console.WriteLine("Endpoint has route pattern: " +
					 routeEndpoint.RoutePattern.RawText);
			}
			foreach (var metadata in endpoint.Metadata) {
				Console.WriteLine($"Endpoint has metadata: {metadata}");
			}
			next(context);
			return Task.CompletedTask;
		};
	}
}
