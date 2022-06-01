using QuizApi.DAL;
using QuizApi.DAL.Models;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuizzApi.DAL {
	public class QuizQuery {

		public QuizQuery( IDbContextFactory<MyDbContext> dbctxFac ) {
			//_dbctx = dbctx;
			_dbctxFac = dbctxFac;
		}

		MyDbContext _dbctx;
		IDbContextFactory<MyDbContext> _dbctxFac;


		[UseDbContext(typeof(MyDbContext))]
		public DbSet<Quiz> Quiz( [ScopedService] MyDbContext dbctx ) {
			return dbctx.Quiz;
		}


		public IEnumerable<Quiz> Quiz2() {
			return _dbctxFac.CreateDbContext().Quiz;
		}


		public string Hello() => "hello";


		public Quiz GetQuiz( int id ) {
			return new Quiz() {
				Id = 1,
				Date = System.DateTime.Now,
				SequenceArray = new int[] { 1, 2, 3, 4, 5 },
				Target = 15,
				SolutionArray = new int[] { 1, 2, 3, 4, 5 }
			};
		}

		//public Quiz GetQuizzes() {
		//	dbctx.Quiz;
		//	return new Quiz() {
		//		Id = 1,
		//		Date = System.DateTime.Now,
		//		SequenceArray = new int[] { 1, 2, 3, 4, 5 },
		//		Target = 15,
		//		SolutionArray = new int[]{ 1, 2, 3, 4, 5 }
		//	};
		//}
	}
}
