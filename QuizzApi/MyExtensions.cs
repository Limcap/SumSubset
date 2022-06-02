using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuizApi.Util {
	public static class MyExtensions {
	
		public static int[] AsIntArray( this string input ) {
			try {
				input = input.Trim('[', ']').Trim();
				var arr = input.Split(new char[]{' ','.',',',',',';','/','\\','-' },StringSplitOptions.RemoveEmptyEntries);
				if (input.IsEmpty()) return new int[0];
				return arr.Select(str => int.Parse(str)).ToArray();
			}
			catch {
				throw new ArgumentException($"A string não é uma sequência válida de números inteiros separados por vírgula: [{input}]");
			}
		}

		public static int[] TryAsIntArray( this string input ) {
			try {	return AsIntArray(input); }
			catch { return null; }
		}
		
		public static string AsString( this int[] a ) => '[' + string.Join(',', a) + ']';
		
		public static R Var<P, R>( this P obj, Func<P, R> func ) => func(obj);
		
		public static bool IsEmpty( this string many ) => many == null || many.Trim().Length == 0;
	}
}
