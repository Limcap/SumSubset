using Microsoft.EntityFrameworkCore;
using QuizApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.DAL {

	public class QuizRequestDAO {

		//public QuizRepository( MyDbContext dbCtx ) {
		//	this.dbCtx = dbCtx;
		//	dbCtx.Database.EnsureCreated();
		//}

		public QuizRequestDAO( IDbContextFactory<MyDbContext> dbCtxFac ) {
			_dbCtxFac = dbCtxFac;
			_dbCtxFac.CreateDbContext().Database.EnsureCreated();
		}


		private MyDbContext CreateDbContext() => _dbCtxFac.CreateDbContext();
		private readonly IDbContextFactory<MyDbContext> _dbCtxFac;
		//private MyDbContext _dbCtx;
		//private MyDbContext dbCtx => _dbCtx ?? (_dbCtx = _dbCtxFac.CreateDbContext());


		public Task<List<QuizRequest>> GetAllAsync() {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.ToListAsync();
			//.Include(x => x.Id)
			//.Include(x => x.Sequence)
			//.Include(x => x.Target)
			//.Include(x => x.Solution)
			//.ToListAsync();
		}


		public List<QuizRequest> GetAll() {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.ToList();
			//.Include(x => x.Id)
			//.Include(x => x.Sequence)
			//.Include(x => x.Target)
			//.Include(x => x.Solution)
			//.ToListAsync();
		}


		public QuizRequest GetById( int id ) {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.SingleOrDefault(x => x.Id == id);
		}


		public Task<QuizRequest> GetByIdAsync( int id ) {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.FirstOrDefaultAsync(m => m.Id == id);
		}


		public async Task<QuizRequest> CreateAsync( QuizRequest quiz ) {
			using var dbCtx = CreateDbContext();
			await dbCtx.QuizRequests.AddAsync(quiz);
			await dbCtx.SaveChangesAsync();
			return quiz;
		}


		public async Task<List<QuizRequest>> GetByDateAsync( DateTime? initialDate, DateTime? finalDate) {
			using var dbCtx = CreateDbContext();
			//if (date1 == null && date2 == null) throw new ArgumentException("É necessário definir as datas");
			if (initialDate == null && finalDate == null) return await dbCtx.QuizRequests.ToListAsync();
			if (initialDate == null) return await dbCtx.QuizRequests.Where(q => q.Date<=finalDate).ToListAsync();
			if (finalDate == null) return await dbCtx.QuizRequests.Where(q => q.Date>=initialDate).ToListAsync();
			if (initialDate > finalDate) (initialDate, finalDate) = (finalDate, initialDate);
			return await dbCtx.QuizRequests.Where(q => q.Date>=initialDate && q.Date <= finalDate).ToListAsync();
		}
	}
}
