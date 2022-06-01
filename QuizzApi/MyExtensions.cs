using System;
using System.Linq;

namespace QuizApi.Util {
	public static class MyExtensions {
		public static int[] AsIntArray( this string s ) => s
			.Trim('[', ']').Trim()
			.Var(s => s.IsEmpty() ? new int[0] : s
				.Split(',')
				.Select(str => int.TryParse(str, out int num) ? num : 0)
				.ToArray()
		);
		public static string AsString( this int[] a ) => '[' + string.Join(',', a) + ']';
		public static R Var<P, R>( this P obj, Func<P, R> func ) => func(obj);
		public static bool IsEmpty( this string many ) => many == null || many.Trim().Length == 0;
	}
}
