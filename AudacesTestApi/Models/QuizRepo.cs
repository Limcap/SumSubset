using AudacesTestApi.DataComm;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudacesTestApi.Models {
	public class QuizRepository {

		public QuizRepository( MyDbContext dbCtx ) {
			this.dbCtx = dbCtx;
			dbCtx.Database.EnsureCreated();
		}


		private readonly MyDbContext dbCtx;


		public async Task<List<Quiz>> GetQuizzes() {
			return await dbCtx.Quiz
				.Include(x => x.Id)
				.Include(x => x.Sequence)
				.Include(x => x.Target)
				.Include(x => x.Solution)
				.ToListAsync();
		}


		public Quiz GetQuizById( int id ) {
			return dbCtx.Quiz.SingleOrDefault(x => x.Id == id);
		}


		public async Task<Quiz> CreateQuiz( Quiz quiz ) {
			await dbCtx.Quiz.AddAsync(quiz);
			await dbCtx.SaveChangesAsync();
			return quiz;

		}
	}
}
