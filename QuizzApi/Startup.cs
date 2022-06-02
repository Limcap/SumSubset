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
using QuizApi.GraphQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi {
	public class Startup {

		public Startup( IConfiguration configuration ) {
			Configuration = configuration;
		}




		public IConfiguration Configuration { get; }
		public string ConnectionString => Configuration["DbConnection:SqliteConnectionString"];




		/// <summary>
		/// Configura os servi�os dispon�veis e inje��o deles como depend�ncias nas controllers.
		/// </summary>
		/// <remarks>
		/// Nota de implementa��o:<br/>
		/// O Controlador de depend�ncia do <see cref="MyDbContext"/> � do tipo Pool, o que permite que inst�ncias 
		/// desse objeto sejam criadas � medida que for necess�rio mas fazendo a reutiliza��o das mesmas para que
		/// caso muitas requests sejam feitas simultaneamente, as int�ncias sejam reutilizadas, evitando a cria��o
		/// e destrui��o de novas inst�ncias a cada request, o que aumenta a performance da aplica��o em tempo e
		/// recursos. Para este projeto em espec�fico isso n�o � necess�rio por�m decidi fazer dessa forma pois
		/// n�o ir� prejudicar e � a forma ideal de se fazer pensando na escalabilidade de um projeto. Principalmente
		/// considerando que objetos do tipo <see cref="DbContext"/> s�o single-threaded, o que significa que n�o
		/// podem ser usado ao mesmo tempo por threads diferentes, ou seja, a cria��o de uma nova insta�ncia para 
		/// acesso simult�neo � obrigat�ria.
		/// </remarks>
		/// <param name="services"></param>
		public void ConfigureServices( IServiceCollection services ) {
			services.AddPooledDbContextFactory<MyDbContext>(opt => opt.UseSqlite(ConnectionString));
			services.AddSingleton<QuizRequestDAO>();
			services.AddControllers();
			services.AddGraphQLServer().AddQueryType<QuizRequestQuery>();
		}




		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IWebHostEnvironment env ) {
			if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
			app.UseRouting();
			app.UseAuthorization();
			app.Use(OutputRequestedEndpointToConsole);
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapGraphQL("/graphql");
				endpoints.MapGet("/help", HelpInfo);
				endpoints.MapGet("/", HelpInfo);
			});
		}




		/// <summary>
		/// Middleware customizado para ficar no in�cio do pipeline para que informa��es sobre a request
		/// sejam mostradas no console.
		/// </summary>
		private Func<RequestDelegate,RequestDelegate> OutputRequestedEndpointToConsole = next => context => {
			var endpoint = context.GetEndpoint();
			//if (endpoint is null) return Task.CompletedTask;
			Console.WriteLine($"{context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
			if (endpoint is RouteEndpoint routeEndpoint) Console.WriteLine($"  Rota: {routeEndpoint.RoutePattern.RawText}");
			Console.WriteLine($"  Endpoint:  {endpoint?.DisplayName}");
			//foreach (var metadata in endpoint.Metadata) Console.WriteLine($"   Metadata: {metadata}");
			return next(context);
		};




		/// <summary>
		/// Middleware que responde � requisi��o com um texto informativo sobre a utiliza��o da API.
		/// O indicado � colocar nas rotas "/" e "/help"
		/// </summary>
		private RequestDelegate HelpInfo = async context => {
			var helpText = File.ReadAllText("ApiInfo.md");
			await context.Response.WriteAsync(helpText);
			//var db = context.RequestServices.GetService(typeof(MyDbContext)) as MyDbContext;
		};
	}
}
