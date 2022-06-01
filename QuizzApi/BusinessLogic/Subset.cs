using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.BusinessLogic {
	public class Subset {

		public Subset( int[] source, int target )
		{ this.source=source; next=0; this.target=target; set=new List<int>(source.Length); shoudDerive=false; } //int pos=0, int nxt=0 //this.nxt=nxt==0?pos+1:nxt;
		int[] source; List<int> set; int next; int target; bool shoudDerive;

		Subset SetNext( int next ) { this.next = next; return this; }
		//Subset AdvanceNext() { next++; return this; }
		//Subset IncludeAt( int pos ) { set.Add(source[pos]); return this; }
		Subset IncludeNext() { set.Add(source[next]); next++; shoudDerive=true; return this; }
		Subset Add( IEnumerable<int> vals ) { set.AddRange(vals); return this; }
		Subset RemoveAt( int pos ) { set.RemoveAt(pos); return this; }
		Subset Clone() => new Subset(source, target).Add(set).SetNext(next);
		void DeriveInto( List<Subset> list )
		{ if (shoudDerive) for (int i = 0; i<set.Count; i++) list.Add(Clone().IncludeNext().RemoveAt(i)); }
		public int Sum => set.Sum();
		public int NextSum => Sum + (next<=source.Length-1 ? source[next] : 0);
		public bool NextSumOverflows => NextSum > target;
		public bool HasNext => next < source.Length;
		public bool IsMatch => Sum == target;

		public override string ToString() {
			return $"  Set: {string.Join(',', set)} ... {(HasNext ? source[next] : 0)}     Next index: {next}     Sum: {Sum}     Next sum: {NextSum}     Overflows: {NextSumOverflows}{(IsMatch ? "     MATCH!" : "")}  ";
		}

		public static int[] GetSubsetOfSum( int[] source, int targetSum ) {
			Array.Sort(source);
			var subsets = new List<Subset>() {new Subset(source,targetSum)};
			//var subset = subsets[0];
			int currentIndex = 0;
			do {
				var subset = subsets[currentIndex];
				while (subset.HasNext) {
					if (subset.NextSumOverflows) break;
					else { subset.IncludeNext(); }
				}
				if (subset.IsMatch) return subset.set.ToArray();
				else subset.DeriveInto(subsets);
				var ok = ReferenceEquals(subset,subsets[0]);
			}
			while (currentIndex++ < subsets.Count-1);
			return null;

			/*
			int sumSubset( Subset sub ) => sub.set.Sum();
			//Subset addSubset( int pos = 0, int nxt = 0 ) { var nss = new Subset(new int[set.Length], pos, nxt); subsets.Add(nss); return nss; }
			Subset newSubset() { var nss = new Subset(source,targetSum); subsets.Add(nss); return nss; }
			void DeriveSubsets() {
				for (int i = 0; i<subset.set.Count-1; i++)
					subset.Clone().IncludeNext().RemoveAt(i);
				//newSubset().AddValues(subset.set).IncludeAt(subset.next).RemoveValueAt(i).SetNext(subset.next+1);
			}
			*/
		}
	}
}
