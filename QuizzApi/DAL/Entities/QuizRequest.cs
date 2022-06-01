using HotChocolate;
using QuizApi.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace QuizApi.DAL.Entities {
	public class QuizRequest {
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		[GraphQLDescription("Data da requisição")]
		public DateTime Date { get; set; }

		[Required]
		[GraphQLIgnore]
		[JsonIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Sequence { get; set; } = String.Empty;

		[Required]
		[GraphQLDescription("Número-alvo para o qual a soma deve ser encontrada")]
		public int Target { get; set; }

		[Required][GraphQLIgnore][JsonIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Solution { get; set; } = String.Empty;

		[NotMapped]
		[GraphQLName("sequence")]
		[GraphQLDescription("Sequencia inicial a partir da qual deve-se encontrar a solução")]
		public int[] SequenceArray { get => Sequence.AsIntArray(); set => Sequence = value.AsString(); }

		[NotMapped]
		[GraphQLName("solution")]
		[GraphQLDescription("Conjunto solução cuja soma é igual ao número-alvo")]
		public int[] SolutionArray { get => Solution.AsIntArray(); set => Solution = value.AsString(); }

	}
}
