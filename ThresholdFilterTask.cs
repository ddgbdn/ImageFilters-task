using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Recognizer
{
    [TestFixture]
    public class RecognizerTests
    {
        static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(0.0, new double[,] { { 123 } }, new double[,] { { 0 } });
            yield return new TestCaseData(1.0, new double[,] { { 123 } }, new double[,] { { 1 } });
            yield return new TestCaseData(0.5, new double[,] { { 1, 2, 2, 3 } }, new double[,] { { 0, 1, 1, 1 } });
	
        }

        [TestCaseSource(nameof(TestCases))]

        public void Test(double whitePixelsFraction, double[,] original, double[,] expected)
        {
            var result = ThresholdFilterTask.ThresholdFilter(original, whitePixelsFraction);
            Assert.AreEqual(result, expected);
        }
    }
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {            
            int x = original.GetLength(0);
            int y = original.GetLength(1);
            var pixels = new List<double>();
            int threshold = (int)(x * y * whitePixelsFraction);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                    pixels.Add(original[i, j]);
            }
            pixels.Sort();
            pixels.Reverse();
            pixels.RemoveRange(threshold, pixels.Count - threshold);
            pixels = pixels.Distinct().ToList();
            return threshold != 0
                ? RecolorImage(original, pixels[pixels.Count - 1])
                : RecolorImage(original, -1);
        }

        private static double[,] RecolorImage (double[,] original, double thresholdValue)
        {
            int x = original.GetLength(0);
            int y = original.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j =0; j < y; j++)
                {
                    if (thresholdValue == -1 || original[i, j] < thresholdValue)
                        original[i, j] = 0.0;
                    else
                        original[i, j] = 1.0;
                }
            }
            return original;
        }
    }
}