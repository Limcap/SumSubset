using AudacesTestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AudacesTestApi.DataComm {
	public class MyDbContext : DbContext {

		public MyDbContext( DbContextOptions<MyDbContext> options ) : base(options) {
		}

		public DbSet<SumProblem> SumProblems { get; set; }

		//protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
		//	optionsBuilder.UseSqlite("Filename=AudacesTestApi.db", options => {
		//		options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
		//	});
		//	base.OnConfiguring(optionsBuilder);
		//}

		protected override void OnModelCreating( ModelBuilder builder ) {
			builder.Entity<SumProblem>().HasKey(m => m.Id);
			base.OnModelCreating(builder);
		}
	}
}
