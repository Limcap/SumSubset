using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.BusinessLogic {
	public class SumSubset {

		public SumSubset( int[] source, int target )
		{ this.source=source; next=0; this.target=target; set=new List<int>(source.Length); canUnravel=false; } //int pos=0, int nxt=0 //this.nxt=nxt==0?pos+1:nxt;
		int[] source; List<int> set; int next; int target; bool canUnravel;



		public static int[] FindSubsetForTargetSum( int[] sourceSequence, int targetSum ) {
			Array.Sort(sourceSequence);
			var subsets = new Queue<SumSubset>();
			subsets.Enqueue(new SumSubset(sourceSequence, targetSum));
			do {
				subsets.TryDequeue(out var subset);
				while (subset.HasNext) {
					if (subset.NextSumOverflows) break;
					else { subset.Advance().AllowUnravel(); }
				}
				if (subset.IsMatch) return subset.set.ToArray();
				else subset.UnravelInto(subsets);
			}
			while (subsets.Count>0);
			return null;
		}



		SumSubset SetNext( int next ) { this.next = next; return this; }
		SumSubset Advance() { if (next>=source.Length) return this; set.Add(source[next]); next++; return this; }
		SumSubset AllowUnravel() { canUnravel=true; return this; }
		SumSubset Add( IEnumerable<int> vals ) { set.AddRange(vals); return this; }
		SumSubset RemoveAt( int pos ) { set.RemoveAt(pos); return this; }
		SumSubset Clone() => new SumSubset(source, target).Add(set).SetNext(next);
		void UnravelInto( Queue<SumSubset> list )
		{ if (canUnravel) for (int i=set.Count-1; i>=0; i--) list.Enqueue(Clone().Advance().RemoveAt(i)); }
		


		int Sum => set.Sum();
		int NextSum => Sum + (next<=source.Length-1 ? source[next] : 0);
		bool NextSumOverflows => NextSum > target;
		bool HasNext => next < source.Length;
		bool IsMatch => Sum == target;



		public override string ToString() {
			return $"  Set: {string.Join(',', set)} ... {(HasNext ? source[next] : 0)}     Next index: {next}     Sum: {Sum}     Next sum: {NextSum}     Overflows: {NextSumOverflows}{(IsMatch ? "     MATCH!" : "")}  ";
		}
	}
}
