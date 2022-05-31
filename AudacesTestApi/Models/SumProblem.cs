using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AudacesTestApi.Models {
	public class SumProblem {
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public DateTime Date { get; set; }

		[Required]
		public string Sequence { get; set; }

		[Required]
		public int Target { get; set; }

		[Required]
		public string Solution { get; set; }

		[NotMapped]
		public int[] SequenceArray {
			get => Sequence.Trim('[').TrimEnd(']').Split(',').Select(str=>int.Parse(str)).ToArray();
			set => Sequence = '[' + string.Join(',',value) + ']';
		}

		[NotMapped]
		public int[] SolutionArray {
			get => Solution.Trim('[').TrimEnd(']').Split(',').Select(str => int.Parse(str)).ToArray();
			set => Solution = '[' + string.Join(',', value) + ']';
		}

	}
}
