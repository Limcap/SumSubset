using HotChocolate;

namespace QuizApi.GraphQL {
	public class QuizResponse {

		public QuizResponse( int[] array ) { Solution = array; }


		//[GraphQLName("HasSolution")]
		[GraphQLDescription("Indica se foi encontrado uma solução")]
		public bool HasSolution { get; set; }


		//[GraphQLName("Solution")]
		[GraphQLDescription("Conjunto solução")]
		public int[] Solution { get; set; }
	}
}
