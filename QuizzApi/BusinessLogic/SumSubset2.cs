using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.BusinessLogic {
	/// <summary>
	/// Classe que faz verificação para encontrar o subconjunto possivel para a soma "target"
	/// </summary>
	/// <remarks>
	/// Funciona, porém só encontra soluções em alguns casos. 
	/// </remarks>
	public class SumSubset2 {
		public static int[] FindSubSetForTargetSum( int[] source, int target) {
			Array.Sort(source);
			int start = 0;
			while (source[start] == 0) start++;
			var stack = new PosStack();
			int sum = 0;
			Pos.source = source;
			int max = source.Length-1;

			add:
			sum = stack.sum;
			int n;
			for (n=start; n<=max; n++) {
				int val = source[n];
				if (val + sum > target || n == max) {
					if (n > start) {
						stack.Push(new Pos(n-1));
						max = n;
					}
				}
			}
			if (stack.sum == target) goto completed;
			else if (stack.sum < target) goto add;
			return null;
			completed:
			return stack.Select(p => p.val).ToArray();
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
	}
}
