using AudacesTestApi.DataComm;
using AudacesTestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudacesTestApi.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class QuizController : Controller {
		private readonly MyDbContext _context;

		public QuizController( MyDbContext context ) {
			_context = context;
		}

		// GET: SumProblems
		[HttpGet]
		public async Task<IActionResult> Index() {
			var result = await _context.Quiz.ToListAsync();
			//return View(result);
			var json = Json(result);
			return Json(result);
		}

		// GET: SumProblems/Details/5
		public async Task<IActionResult> Details( int? id ) {
			if (id == null) {
				return NotFound();
			}

			var sumProblem = await _context.Quiz
					 .FirstOrDefaultAsync(m => m.Id == id);
			if (sumProblem == null) {
				return NotFound();
			}

			return View(sumProblem);
		}

		// GET: SumProblems/Create
		public IActionResult Create() {
			return View();
		}

		// POST: SumProblems/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create( [Bind("Id,Date,Sequence,Target,Solution")] Quiz sumProblem ) {
			if (ModelState.IsValid) {
				_context.Add(sumProblem);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(sumProblem);
		}

		// GET: SumProblems/Edit/5
		public async Task<IActionResult> Edit( int? id ) {
			if (id == null) {
				return NotFound();
			}

			var sumProblem = await _context.Quiz.FindAsync(id);
			if (sumProblem == null) {
				return NotFound();
			}
			return View(sumProblem);
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
					_context.Update(sumProblem);
					await _context.SaveChangesAsync();
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

			var sumProblem = await _context.Quiz
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
			var sumProblem = await _context.Quiz.FindAsync(id);
			_context.Quiz.Remove(sumProblem);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SumProblemExists( int id ) {
			return _context.Quiz.Any(e => e.Id == id);
		}
	}
}
