using GraphQL.Types;
using System;

namespace AudacesTestApi.GraphQL {
	public class QuizSchema : Schema {
		public QuizSchema(IServiceProvider provider ) {
			//Query = serviceProvider.GetService<QuizType>();
		}
	}
}
