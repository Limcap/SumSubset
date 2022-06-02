using HotChocolate;
using HotChocolate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApi.BusinessLogic;
using QuizApi.DAL;
using QuizApi.Models;
using QuizApi.Util;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace QuizApi.Controllers {

	//[Route("rest/[controller]")]
	[Route("rest")]
	[ApiController]
	public partial class QuizRequestController : Controller {


		public QuizRequestController( QuizRequestDAO quizDAO ) {
			_quizDAO = quizDAO;
		}




		private readonly QuizRequestDAO _quizDAO;




		[HttpGet] // GET: rest
		public async Task<IActionResult> Index() {
			var result = await _quizDAO.GetAllAsync();
			return Json(result);
		}




		[HttpGet("previousRequest/id/{id}")] // GET: rest/quiz/5
		//[HttpGet("previousRequests")] // GET: rest/quiz?id=5
		public async Task<IActionResult> PreviousRequest( int? id ) {
			if (id == null) return Json(await _quizDAO.GetAllAsync());
			var sumProblem = await _quizDAO.GetByIdAsync( id.Value );
			if (sumProblem == null) return Json(new { });
			return Json(sumProblem);
		}




		[HttpGet("previousRequests")] // GET: rest/quiz?id=5
		public async Task<IActionResult> PreviousRequest( DateTime? initialDate = null, DateTime? finalDate = null ) {
			var result = await _quizDAO.GetByDateAsync(initialDate,finalDate);
			return Json(result);
		}




		[HttpGet("solveQuiz/{sequence}/{target}")] // GET: rest/quiz?id=5
		[HttpGet("solveQuiz")] // GET: rest/quiz?id=5
		public async Task<IActionResult> SolveQuizz( string sequence, int target ) {
			var arr = sequence.TryAsIntArray();
			RestResponse response;
			if (arr == null) response = new RestResponse($"A sequencia de numeros informada e invalida: [{sequence}]", null);
			else response = new RestResponse(null, "Nao implementado");
			var solution = await Task.Run(()=>SumSubset1.FindSubsetForTargetSum(arr, target) ?? SumSubset2.FindSubSetForTargetSum(arr,target) ?? new int[0]);
			var quiz = new QuizRequest() { Date = DateTime.Now, SequenceArray=arr, Target=target, SolutionArray=solution };
			var r = _quizDAO.CreateAsync( quiz );
			return Json(response);
		}




		public struct RestResponse {
			public RestResponse( string error, string data ) { this.error = error; this.data = data; }
			public string error { get; set; }
			public string data { get; set; }
		}
	}
}
