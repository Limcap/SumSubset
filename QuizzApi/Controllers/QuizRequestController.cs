using HotChocolate;
using HotChocolate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.DAL;
using QuizApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuizApi.Controllers {

	//[Route("rest/[controller]")]
	[Route("rest")]
	[ApiController]
	public class QuizRequestController : Controller {

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


		public QuizRequestController( QuizRequestDAO quizDAO ) {
			_quizDAO = quizDAO;
		}



		//private readonly IDbContextFactory<MyDbContext> _dbctxFac;
		//private MyDbContext _dbctx => _dbctxFac.CreateDbContext();
		private readonly QuizRequestDAO _quizDAO;




		[HttpGet] // GET: rest
		public async Task<IActionResult> Index() {
			var result = await _quizDAO.GetAllAsync();
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
			if (id == null) return Json(await _quizDAO.GetAllAsync());
			var sumProblem = await _quizDAO.GetByIdAsync( id.Value );
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
		[HttpGet("create")] // POST: rest/create
		public async Task<IActionResult> Create( string sequence, int target ) {
			int[] seq = toint(sequence);
			var a = GFG.isSubsetSum2(seq, seq.Length, target);
			if (ModelState.IsValid) {
				var quiz = new QuizRequest(){SequenceArray=seq, Target=target, Date=DateTime.Now, SolutionArray=new int[0]};
				var result = await _quizDAO.CreateAsync(quiz);
				return Json(new { Sucess = true, Id = result.Id });
			}
			return Json(new { Error = "Model is not valid" });
		}

		private int[] toint( string sequence ) {
			return sequence.Split(',').Select(n => int.Parse(n)).ToArray();
		}
		//bool checkSum( int[] arr, int i, int n, int target ) {
		//	if (target==0)
		//		return true;
		//	if (i>=n or target<0)
		//      return false;

		//	return (checkSum(arr, i+1, n, target) or // don't include current value and move to next
		//			  arr[i]!=0 and   // include only if non-zero value
		//			  (checkSum(arr, i, n, target-arr[i]) or // include current value 

		//			  checkSum(arr, i+1, n, target-arr[i]))); // include current value and move to next
		//}
		class GFG {
			// Returns true if there is a subset of set[] with sum
			// equal to given sum
			public static bool isSubsetSum( int[] set, int n, int sum ) {
				// Base Cases
				if (sum == 0)
					return true;
				if (n == 0 && sum != 0)
					return false;

				// If last element is greater than sum,
				// then ignore it
				if (set[n - 1] > sum)
					return isSubsetSum(set, n - 1, sum);

				/* else, check if sum can be obtained 
				by any of the following
				(a) including the last element
				(b) excluding the last element */
				return isSubsetSum(set, n - 1, sum)
						|| isSubsetSum(set, n - 1, sum - set[n - 1]);
			}


			public static bool isSubsetSum2( int[] set, int n, int sum ) {
				// The value of subset[i][j] will be true if there
				// is a subset of set[0..j-1] with sum equal to i
				bool[, ] subset = new bool[sum + 1, n + 1];

				// If sum is 0, then answer is true
				for (int i = 0; i <= n; i++)
					subset[0, i] = true;

				// If sum is not 0 and set is empty, then answer is false
				for (int i = 1; i <= sum; i++)
					subset[i, 0] = false;

				// Fill the subset table in bottom up manner
				for (int i = 1; i <= sum; i++) {
					for (int j = 1; j <= n; j++) {
						subset[i, j] = subset[i, j - 1];
						if (i >= set[j - 1])
							subset[i, j] = subset[i, j] || subset[i - set[j - 1], j - 1];
					}
				}

				return subset[sum, n];
			}
		}
	}
}
