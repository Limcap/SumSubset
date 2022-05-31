using AudacesTestApi.Models;
using GraphQL.Types;

namespace AudacesTestApi.GraphQL {

	public class QuizType : ObjectGraphType<Quiz> {

		public QuizType() {
			Name = nameof(Quiz);
			Description = "Um quiz de soma";

			Field(x => x.Id);
			Field(x => x.Sequence).Description("A sequência de números disponíveis para resolução");
			Field(x => x.Target).Description("O número-alvo para o qual deve ser encontrado a soma");
			Field(x => x.Solution).Description("O conjunto de números da solução do problema");
			//Field( type: typeof(ListGraphType<>), resolve: m => m.Source.Target);
		}
	}
}
