using AudacesTestApi.DataComm;
using AudacesTestApi.Models;
using HotChocolate;
using System.Linq;

namespace AudacesTestApi.GraphQL {
	public class Query {
		
		public Query( MyDbContext dbctx ) {
			this.dbctx = dbctx;
		}

		MyDbContext dbctx;
		//public IQueryable<Quiz> GetQuiz( [Service] MyDbContext ctx ) {
		//	return dbctx.Quiz;
		//}

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
