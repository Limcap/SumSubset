using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using QuizApi.BusinessLogic;
using QuizApi.DAL;
using QuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.GraphQL {

	/// <summary>
	/// Classe que fornece os métodos de queries possíveis para a API do GraphQL
	/// </summary>
	/// <remarks>
	/// Notas de implementação:<br/>
	/// Preferi utilizar a injeção da dependência <see cref="QuizRequestDAO"/> via construtor da classe ao invés de
	/// usar a abordagem '[ScopedService]' nos contrutores dos métodos, por dois motivos:<br/>
	/// 1 - Não é necessário usar um escopo diferente para este objeto nas chamadas de cada método, mesmo que sejam feitas
	/// simultaneamente, este problema só acontece com <see cref="DbContext"/> e já foi resolvido dentro da classe DAO em
	/// questão através da injeção de <see cref="IDbContextFactory{TContext}"/>.<br/>
	/// 2 - O código fica mais claro e o entendimento é mais rápido pois fica menor e mais simples.
	/// </remarks>
	public partial class QuizRequestQuery {

		public QuizRequestQuery( QuizRequestDAO quizDAO ) {
			_quizDAO = quizDAO;
		}

		private readonly QuizRequestDAO _quizDAO;


		// Abordagem via annotation [ScopedService] preterida a favor da injeção de dependencia via construturo de classe
		//[UseDbContext(typeof(MyDbContext))]
		//public DbSet<QuizRequest> Quiz( [ScopedService] MyDbContext dbctx ) {
		//	return dbctx.Quiz;
		//}


		[GraphQLDescription("Obtem uma requisição anterior correspondente ao 'id' informado")]
		public async Task<QuizRequest> GetPreviousRequest( int id ) {
			var result = await _quizDAO.GetByIdAsync( id );
			return result;
		}



		[GraphQLDescription("Obtem as requisições já realizadas, dentro de um período de tempo especificado")]
		public async Task<List<QuizRequest>> GetPreviousRequests( DateTime? initialDate = null, DateTime? finalDate = null ) {
			var result = await _quizDAO.GetByDateAsync(initialDate, finalDate);
			return result;
		}



		[GraphQLDescription("Obtém a solução para um quiz cuja sequência e alvo são informados")]
		public async Task<QuizResponse> SolveQuiz( int[] sequence, int target ) {
			var solution = await Task.Run(()=>SumSubset1.FindSubsetForTargetSum(sequence, target) ?? SumSubset2.FindSubSetForTargetSum(sequence,target) ?? new int[0]);
			var quiz = new QuizRequest() { Date = DateTime.Now, SequenceArray=sequence, Target=target, SolutionArray=solution };
			var result = await _quizDAO.CreateAsync(quiz);
			return new QuizResponse(result.SolutionArray);
		}
	}
}
