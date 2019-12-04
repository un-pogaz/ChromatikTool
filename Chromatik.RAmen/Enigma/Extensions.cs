using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{

	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> collection, int ofSize)
		{
			if(ofSize == 0)
			{
				yield return new List<T>();
			}
			else
			{
				var index = 0;
				foreach(var elem in collection)
				{
					index++;
					foreach(var subCollection in collection.Skip(index).Combinations(ofSize - 1))
					{
						yield return (new List<T>() { elem }).Concat(subCollection);
					}
				}
			}
		}

		public static string ToCsv<T>(this IEnumerable<T> collection)
		{
			return string.Join(",", collection);
		}
	}
}
