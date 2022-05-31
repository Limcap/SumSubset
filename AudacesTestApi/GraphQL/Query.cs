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
		//	return context.Quiz;
		//}

		public IQueryable<Quiz> GetQuiz() {
			return dbctx.Quiz;
		}
	}
}
