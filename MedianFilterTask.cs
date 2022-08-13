using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
			int x = original.GetLength(0);
			int y = original.GetLength(1);
			double[,] result = new double[x, y];
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					result[i, j] = GetMedian(original, i, j);
				}
			}
			return result;
		}

		private static double GetMedian(double[,] original, int x, int y)
		{
			int row = original.GetLength(0);
			int col = original.GetLength(1);
			var area = new List<double>();
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					if (i < 0 || i >= row)
						break;
					if (j < 0 || j >= col)
						continue;
					area.Add(original[i, j]);
				}
			}
			area.Sort();
			return area.Count() % 2 == 0
				? (area[area.Count() / 2 - 1] + area[area.Count() / 2]) / 2
				: area[(area.Count() - 1) / 2];
		}
	}
}