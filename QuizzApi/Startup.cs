using QuizApi.DAL;
//using GraphQL;
//using GraphQL.MicrosoftDI;
//using GraphQL.Server;
//using GraphQL.SystemTextJson;
//using GraphQL.Types;
using HotChocolate;
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
using QuizApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizApi.GraphQL;

namespace QuizApi {
	public class Startup {

		public Startup( IConfiguration configuration ) {
			Configuration = configuration;
		}




		public IConfiguration Configuration { get; }
		public string ConnectionString => Configuration["DbConnection:SqliteConnectionString"];




		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services ) {
			//services.AddDbContext<MyDbContext>(options => options.UseSqlite(ConnectionString));
			//services.AddDbContextPool<MyDbContext>(options => options.UseSqlite(ConnectionString));
			services.AddPooledDbContextFactory<MyDbContext>(opt => opt.UseSqlite(ConnectionString));
			
			services.AddSingleton<QuizRequestDAO>();
			
			services.AddControllers();
			
			services.AddGraphQLServer().AddQueryType<QuizRequestQuery>();
			
			//services.AddMvc();


			//services.AddScoped<IServiceProvider>(x => new FuncDependencyResolver(x.GetRequiredService));
			//services.AddSingleton(s => new (( type ) => (IGraphType)s.GetRequiredService(type)));

			//services.AddScoped<GraphQL.QuizSchema>();
			//services.AddScoped<GraphQL.Query>();

			/*
			services.AddGraphQL(opt => {
				// Load GraphQL Server configurations
				var graphQLOptions = Configuration .GetSection("GraphQL").Get<GraphQLOptions>();
				opt.ComplexityConfiguration = graphQLOptions.ComplexityConfiguration;
				opt.EnableMetrics = graphQLOptions.EnableMetrics;
				//opt.ExposeExceptions = true;
				// Log errors
				//var logger = provider.GetRequiredService<ILogger<Startup>>();
				//options.UnhandledExceptionDelegate = ctx =>
				//	 logger.LogError("{Error} occurred", ctx.OriginalException.Message);
			});
			services.AddGraphTypes(ServiceLifetime.Scoped);
			services.AddDataLoader();
			*/
		}




		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
			if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
			app.UseRouting();
			app.UseAuthorization();
			//app.Use(OutputRequestedEndpointToConsole);
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapGraphQL("/graphql");
				endpoints.MapGet("/help", async context => {
					await context.Response.WriteAsync("Hello World!");
					var db = context.RequestServices.GetService(typeof(MyDbContext)) as MyDbContext;

				});
			});
			//app.UseGraphQLAltair("/altair");
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
	public class Query1 {
		public string Hello() { return "How are you?"; }
	}
}
