using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.BusinessLogic {
	/// <summary>
	/// Classe que faz a verificação para encontrar o subconjunto possivel para a soma "target"
	/// </summary>
	/// <remarks>
	/// Funciona, porém só encontra soluções em alguns casos. 
	/// </remarks>
	public class SumSubset {

		public SumSubset( int[] source, int target )
		{ this.source=source; next=0; this.target=target; set=new List<int>(source.Length); mustUnravel=false; } //int pos=0, int nxt=0 //this.nxt=nxt==0?pos+1:nxt;
		int[] source; List<int> set; int next; int target; bool mustUnravel;



		public static int[] FindSubsetForTargetSum( int[] sourceSequence, int targetSum ) {
			Array.Sort(sourceSequence);
			var subsets = new Queue<SumSubset>();
			subsets.Enqueue(new SumSubset(sourceSequence, targetSum));
			do {
				subsets.TryDequeue(out var subset);
				while (subset.HasNext) {
					if (subset.Advance().Overflows) break;
					else { subset.mustUnravel = true; }
				}
				if (subset.IsMatch) return subset.set.ToArray();
				else if (subset.mustUnravel) subset.UnravelInto(subsets);
			}
			while (subsets.Count>0);
			return null;
		}



		SumSubset SetNext( int next ) { this.next = next; return this; }
		SumSubset Advance() { if (next>=source.Length) return this; set.Add(source[next]); next++; return this; }
		SumSubset Add( IEnumerable<int> vals ) { set.AddRange(vals); return this; }
		SumSubset RemoveAt( int pos ) { set.RemoveAt(pos); return this; }
		SumSubset Clone() => new SumSubset(source, target).Add(set).SetNext(next);
		void UnravelInto( Queue<SumSubset> list ) {
			for (int i = set.Count-1; i>=0; i--)
			{ var newSub = Clone().Advance().RemoveAt(i); if(newSub.HasNext) list.Enqueue(newSub); if (newSub.IsMatch) break; }
		}
		


		int Sum => set.Sum();
		bool Overflows => Sum > target;
		bool HasNext => next < source.Length;
		bool IsMatch => Sum == target;



		public override string ToString() {
			return $"  Set: {string.Join(',', set)}     Sum: {Sum}     Next index: {(next<source.Length ? next.ToString() : "X")}     Overflows: {Overflows}{(IsMatch ? "     MATCH!" : "")}  ";
		}
	}
}
