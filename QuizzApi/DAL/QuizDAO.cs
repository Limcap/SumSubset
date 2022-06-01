using Microsoft.EntityFrameworkCore;
using QuizApi.DAL;
using QuizApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzApi.DAL {

	public interface IQuizDAO {
		List<Quiz> GetQuizzes();
		Task<List<Quiz>> GetQuizzesAsync();
		Quiz GetQuizById( int id );
		Task<Quiz> GetQuizByIdAsync( int id );
		Task<Quiz> CreateQuizAsync( Quiz quiz );
		Task<Quiz> GetQuizRequestByDate( DateTime startDate, DateTime finalDate );
	}



	public class QuizDAO : IQuizDAO {

		//public QuizRepository( MyDbContext dbCtx ) {
		//	this.dbCtx = dbCtx;
		//	dbCtx.Database.EnsureCreated();
		//}

		public QuizDAO( IDbContextFactory<MyDbContext> dbCtxFac ) {
			_dbCtxFac = dbCtxFac;
			_dbCtxFac.CreateDbContext().Database.EnsureCreated();
		}


		private MyDbContext CreateDbContext() => _dbCtxFac.CreateDbContext();
		private readonly IDbContextFactory<MyDbContext> _dbCtxFac;
		//private MyDbContext _dbCtx;
		//private MyDbContext dbCtx => _dbCtx ?? (_dbCtx = _dbCtxFac.CreateDbContext());


		public Task<List<Quiz>> GetQuizzesAsync() {
			using var dbCtx = CreateDbContext();
			return dbCtx.Quiz.ToListAsync();
			//.Include(x => x.Id)
			//.Include(x => x.Sequence)
			//.Include(x => x.Target)
			//.Include(x => x.Solution)
			//.ToListAsync();
		}


		public List<Quiz> GetQuizzes() {
			using var dbCtx = CreateDbContext();
			return dbCtx.Quiz.ToList();
				//.Include(x => x.Id)
				//.Include(x => x.Sequence)
				//.Include(x => x.Target)
				//.Include(x => x.Solution)
				//.ToListAsync();
		}


		public Quiz GetQuizById( int id ) {
			using var dbCtx = CreateDbContext();
			return dbCtx.Quiz.SingleOrDefault(x => x.Id == id);
		}


		public Task<Quiz> GetQuizByIdAsync( int id ) {
			using var dbCtx = CreateDbContext();
			return dbCtx.Quiz.FirstOrDefaultAsync( m => m.Id == id);
		}


		public async Task<Quiz> CreateQuizAsync( Quiz quiz ) {
			using var dbCtx = CreateDbContext();
			await dbCtx.Quiz.AddAsync(quiz);
			await dbCtx.SaveChangesAsync();
			return quiz;
		}


		public Task<Quiz> GetQuizRequestByDate( DateTime startDate, DateTime finalDate ) {
			throw new NotImplementedException();
		}
	}
}
