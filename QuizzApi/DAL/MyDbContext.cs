﻿using QuizApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QuizApi.DAL {
	public class MyDbContext : DbContext {

		public MyDbContext( DbContextOptions<MyDbContext> options ) : base(options) {
		}

		public DbSet<QuizRequest> QuizRequests { get; set; }

		//protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
		//	optionsBuilder.UseSqlite("Filename=AudacesTestApi.db", options => {
		//		options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
		//	});
		//	base.OnConfiguring(optionsBuilder);
		//}

		//protected override void OnModelCreating( ModelBuilder builder ) {
		//	builder.Entity<QuizRequest>().HasKey(m => m.Id);
		//	base.OnModelCreating(builder);
		//}
	}
}
