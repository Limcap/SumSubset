using HotChocolate;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace QuizApi.DAL.Models {
	public class Quiz {
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		[GraphQLDescription("Data da requisição")]
		public DateTime Date { get; set; }

		[Required][GraphQLIgnore][JsonIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Sequence { get; set; }

		[Required]
		[GraphQLDescription("Número-alvo para o qual a soma deve ser encontrada")]
		public int Target { get; set; }

		[Required][GraphQLIgnore][JsonIgnore]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Solution { get; set; }

		[NotMapped]
		[GraphQLDescription("Sequencia inicial a partir da qual deve-se encontrar a solução")]
		public int[] SequenceArray {
			get => Sequence.Trim('[').TrimEnd(']').Split(',').Select(str=>int.Parse(str)).ToArray();
			set => Sequence = '[' + string.Join(',',value) + ']';
		}

		[NotMapped]
		[GraphQLDescription("Conjunto solução cuja soma é igual ao número-alvo")]
		public int[] SolutionArray {
			get => Solution.Trim('[').TrimEnd(']').Split(',').Select(str => int.Parse(str)).ToArray();
			set => Solution = '[' + string.Join(',', value) + ']';
		}

	}
}
