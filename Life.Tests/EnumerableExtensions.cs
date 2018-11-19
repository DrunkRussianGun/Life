using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Life.Tests
{
	internal static class EnumerableExtensions
	{
		public static IEnumerable<TestCaseData> ToTestCaseSource<T>(this IEnumerable<T> data)
		{
			return data
				.Select(arg => new TestCaseData(arg))
				.ToArray();
		}
	}
}
