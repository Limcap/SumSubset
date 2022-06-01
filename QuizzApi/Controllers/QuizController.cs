using QuizApi.DAL;
using QuizApi.DAL.Models;
using HotChocolate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Data;
using QuizzApi.DAL;
using System.Net.Http;

namespace QuizApi.Controllers {
	
	//[Route("rest/[controller]")]
	[ApiController]
	[Route("rest")]
	public class QuizController : Controller {

		//public QuizController( IServiceScopeFactory scopeFac, IServiceProvider provider, IDbContextFactory<MyDbContext> dbctxFac ) {
		//	//_dbctx = context;
		//	using (var scope = scopeFac.CreateScope()) {
		//		//_dbctx = scope.ServiceProvider.GetRequiredService<MyDbContext>();
		//		_dbctx = scope.ServiceProvider.GetService<MyDbContext>();
		//		var db = provider.GetService<MyDbContext>();
		//		provider.CreateScope();
		//		var db1 = dbctxFac.CreateDbContext();
		//		db1.Dispose();
		//	}
		//}


		//public QuizController( IDbContextFactory<MyDbContext> dbctxFac ) {
		//	_dbctxFac = dbctxFac;
		//	_dbctxFac.CreateDbContext().Database.EnsureCreated();
		//}


		public QuizController( IQuizDAO quizDAO ) {
			_quizDAO = quizDAO;
		}



		//private readonly IDbContextFactory<MyDbContext> _dbctxFac;
		//private MyDbContext _dbctx => _dbctxFac.CreateDbContext();
		private readonly IQuizDAO _quizDAO;




		[HttpGet] // GET: rest
		public async Task<IActionResult> Index() {
			var result = await _quizDAO.GetQuizzesAsync();
			return Json(result);
			//return View(result);
		}
		//public async Task<IActionResult> Index() {
		//	using var db = _dbctxFac.CreateDbContext();
		//	var result = await db.Quiz.ToListAsync();
		//	//return View(result);
		//	var json = Json(result);
		//	return Json(result);
		//}




		[HttpGet("quiz/{id}")] // GET: rest/quiz/5
		[HttpGet("quiz")] // GET: rest/quiz?id=5
		public async Task<IActionResult> Details( int? id ) {
			if (id == null) return Json(await _quizDAO.GetQuizzesAsync());
			var sumProblem = await _quizDAO.GetQuizByIdAsync( id.Value );
			if (sumProblem == null) return Json(new { });
			return Json(sumProblem);
		}
		//public async Task<IActionResult> Details( int? id ) {
		//	using var db = _dbctxFac.CreateDbContext();
		//	//if (id == null) return NotFound();
		//	if (id == null) return Json(await db.Quiz.ToListAsync());
		//	var sumProblem = await db.Quiz.FirstOrDefaultAsync(m => m.Id == id);
		//	if (sumProblem == null) return Json(new{});
		//	return Json(sumProblem);
		//}




		// GET: SumProblems/Create
		//public async Task<IActionResult> Create( Quiz quiz ) {
		//	var result = await _quizDAO.CreateQuizAsync(quiz);
		//	return Json(result);
		//	//using var db = _dbctxFac.CreateDbContext();
		//	//return View();
		//}
		public class QuizzRequest { string sequence; string target; }
		[HttpPost("create")] // POST: rest/create
		//public async Task<IActionResult> Create([Bind("sequence,target")] QuizzRequest d ) { 
		public async Task<IActionResult> Create(int sequence, int target) {
			var key = HttpContext.Request.Form.Keys;
			if (ModelState.IsValid) {
				var result = false;
				//var result = await _quizDAO.CreateQuizAsync(quiz);
				return Json(result);
			}
			return Json(new { Error = "Model is not valid" });
		}


		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost("create2")] // POST: rest/create
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create2( [Bind("Id,Date,Sequence,Target,Solution")] Quiz quiz ) {
			if (ModelState.IsValid) {
				var result = await _quizDAO.CreateQuizAsync(quiz);
				return Json(result);
			}
			return Json(new { Error = "Model is not valid" });
		}
		//public async Task<IActionResult> Create( [Bind("Id,Date,Sequence,Target,Solution")] Quiz quiz ) {
		//	if (ModelState.IsValid) {
		//		_dbctx.Add(quiz);
		//		await _dbctx.SaveChangesAsync();
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(quiz);
		//}



		/*
		// GET: SumProblems/Edit/5
		public async Task<IActionResult> Edit( int? id ) {
			if (id == null) { return Json(NotFound()); }
			var quiz = await _dbctx.Quiz.FindAsync(id);
			if (quiz == null) {
				return NotFound();
			}
			return View(quiz);
		}




		// POST: SumProblems/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit( int id, [Bind("Id,Date,Sequence,Target,Solution")] Quiz sumProblem ) {
			if (id != sumProblem.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					_dbctx.Update(sumProblem);
					await _dbctx.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException) {
					if (!SumProblemExists(sumProblem.Id)) {
						return NotFound();
					}
					else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(sumProblem);
		}

		// GET: SumProblems/Delete/5
		public async Task<IActionResult> Delete( int? id ) {
			if (id == null) {
				return NotFound();
			}

			var sumProblem = await _dbctx.Quiz
					 .FirstOrDefaultAsync(m => m.Id == id);
			if (sumProblem == null) {
				return NotFound();
			}

			return View(sumProblem);
		}

		// POST: SumProblems/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed( int id ) {
			var sumProblem = await _dbctx.Quiz.FindAsync(id);
			_dbctx.Quiz.Remove(sumProblem);
			await _dbctx.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SumProblemExists( int id ) {
			return _dbctx.Quiz.Any(e => e.Id == id);
		}
		*/
	}
}
