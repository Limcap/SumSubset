using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.BusinessLogic {
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








	/// <summary>
	/// Tentativa de implementação de algoritimo para achar o subconjunto da soma.
	/// INCOMPLETO
	/// </summary>
	public class Subset_Tentativa2 {
		public static void Find( int[] source, int target ) {
			Array.Sort(source);
			int start = 0;
			while (source[start] == 0) start++;
			//int FindLastViableIndex() { for (var j = source.Length-1; j>=0; j--) if (source[j] <= target) return j; return 0; }
			var stack = new PosStack();
			int sum = 0;
			Pos.source = source;
			int max = source.Length-1;
			int stackIdx = 1;

			add:
			sum = stack.sum;
			int n;
			for (n=start; n<max; n++) {
				int val = source[n];
				if (val + sum > target) {
					if (n > start) {
						stack.Push(new Pos(n-1));
						max = n;
					}
					else {
						stackIdx = 1;
						goto find_adjustable_element;
					}
				}
			}
			if (stack.sum == target) goto completed;
			else if (stack.sum < target) goto add;

			find_adjustable_element:
			if (stack.Count < 2) goto impossible;
			//if( stack.Count > 2) 
			int s = 1;
			while (s < stack.Count-1 && stack.At(s+1).idx == stack.At(s).idx+1) s++;
			if (stack.At(s).idx == stack.At(s-1).idx-1) goto remove_last;
			var pos = stack.At(s);
			//void AllElementsTogether() {
			//	var idxs = stack.Select(e => e.idx).ToArray();
			//	for
			//}
			max = Math.Min(max, s<stack.Count-2 ? stack.At(s+1).idx : max);
			sum = stack.sum;
			int m;
			for (m=pos.idx; m<max; m++) {
				int val = source[m];
				if (val + sum > target) {
					if (m > start) {
						stack.Push(new Pos(m-1));
						max = m;
					}
					else {
						stackIdx = 1;
						goto find_adjustable_element;
					}
				}
			}

			var posB = stack.ElementAt(stackIdx);
			if (posB.idx-1 == pos.idx)
				posB.idx--;

			remove_last:


			completed:
			var f = stack;

			impossible:
			var gf = 2;

		}

		public class Pos {
			public Pos( int idx ) => this.idx = idx;
			public static int[] source;
			public int idx;
			public int val => source[idx];
			public static implicit operator int( Pos e ) => e.val;
		}
		public class PosStack : Stack<Pos> {
			public int sum { get { var s = 0; foreach (var e in this) s+=e.val; return s; } }
			public Pos At( int idx ) => this.ElementAt(idx);
		}
		public static int SumQ( Queue<Pos> q ) { int val=0; foreach (var e in q) val+=e; return val; }
	}
}
