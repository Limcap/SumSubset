using HotChocolate;

namespace QuizApi.GraphQL {

	/// <summary>
	/// Classe que representa uma resposta da query de GraphQL executada por <see cref="QuizRequestQuery.SolveQuiz"/>
	/// </summary>
	public class QuizResponse {

		public QuizResponse( int[] array ) { Solution = array; if (array.Length>0) HasSolution = true; }


		//[GraphQLName("HasSolution")]
		[GraphQLDescription("Indica se foi encontrado uma solução")]
		public bool HasSolution { get; set; }


		//[GraphQLName("Solution")]
		[GraphQLDescription("Conjunto solução")]
		public int[] Solution { get; set; }
	}
}
