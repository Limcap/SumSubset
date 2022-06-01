using Microsoft.EntityFrameworkCore;
using QuizApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.DAL {

	/// <summary>
	/// Classe que faz o acesso à base de dados.
	/// </summary>
	/// <remarks>
	/// Utiliza o EntityFramework.<br/>
	/// Notas de implementação:
	/// O acesso ao contexto de base de dados é feito através da injeção de um <see cref="IDbContextFactory{TContext}"/> ao invés
	/// de da injeção direta de um <see cref="DbContext"/>. Isso permite que cada método crie seu próprio contexto ao acessar este
	/// objeto permitindo que vários acessos simultâneos sejam feitos sem problemas, uma vez que o <see cref="DbContext"/> em si 
	/// é single threaded.
	/// </remarks>
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


		public List<QuizRequest> GetAll() {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.ToList();
			//.Include(x => x.Id)
			//.Include(x => x.Sequence)
			//.Include(x => x.Target)
			//.Include(x => x.Solution)
			//.ToListAsync();
		}


		public Task<List<QuizRequest>> GetAllAsync() {
			using var dbCtx = CreateDbContext();
			return dbCtx.QuizRequests.ToListAsync();
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


		public async Task<List<QuizRequest>> GetByDateAsync( DateTime? initialDate, DateTime? finalDate) {
			using var dbCtx = CreateDbContext();
			//if (date1 == null && date2 == null) throw new ArgumentException("É necessário definir as datas");
			if (initialDate == null && finalDate == null) return await dbCtx.QuizRequests.ToListAsync();
			if (initialDate == null) return await dbCtx.QuizRequests.Where(q => q.Date<=finalDate).ToListAsync();
			if (finalDate == null) return await dbCtx.QuizRequests.Where(q => q.Date>=initialDate).ToListAsync();
			if (initialDate > finalDate) (initialDate, finalDate) = (finalDate, initialDate);
			return await dbCtx.QuizRequests.Where(q => q.Date>=initialDate && q.Date <= finalDate).ToListAsync();
		}


		public async Task<QuizRequest> CreateAsync( QuizRequest quiz ) {
			using var dbCtx = CreateDbContext();
			await dbCtx.QuizRequests.AddAsync(quiz);
			await dbCtx.SaveChangesAsync();
			return quiz;
		}
	}
}
