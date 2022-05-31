using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace AudacesTestApi.Models {
	public class Quiz {
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public DateTime Date { get; set; }

		[Required]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Sequence { get; set; }

		[Required]
		public int Target { get; set; }

		[Required]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string Solution { get; set; }

		[NotMapped]
		[JsonIgnore]
		public int[] SequenceArray {
			get => Sequence.Trim('[').TrimEnd(']').Split(',').Select(str=>int.Parse(str)).ToArray();
			set => Sequence = '[' + string.Join(',',value) + ']';
		}

		[NotMapped]
		[JsonIgnore]
		public int[] SolutionArray {
			get => Solution.Trim('[').TrimEnd(']').Split(',').Select(str => int.Parse(str)).ToArray();
			set => Solution = '[' + string.Join(',', value) + ']';
		}

	}
}
