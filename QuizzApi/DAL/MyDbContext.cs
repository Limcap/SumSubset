using QuizApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QuizApi.DAL {

	/// <summary>
	/// Classe que representa o objeto da base de dados.
	/// </summary>
	/// <remarks>
	/// Utiliza <see cref="Microsoft.EntityFrameworkCore"/><br/>
	/// Utilizado pelas classes de DAO.<br/>
	/// </remarks>
	/// <seealso cref="QuizRequestDAO"/>
	public class MyDbContext : DbContext {

		public MyDbContext( DbContextOptions<MyDbContext> options ) : base(options) {
		}

		public DbSet<QuizRequest> QuizRequests { get; set; }
	}
}
