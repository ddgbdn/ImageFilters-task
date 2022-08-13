using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            int border = (sx.GetLength(0) - 1) / 2;
            var result = new double[width, height];
            for (int x = border; x < width - border; x++)
                for (int y = border; y < height - border; y++)
                {
                   result[x, y] = GetPixel(g, sx, x, y, border);
                }
            return result;
        }
        
        private static double GetPixel (double[,] g, double [,] sx, int x, int y, int border)
        {
            double gx = 0.0, gy = 0.0;            
            for (int i = -border; i <= border; i++)
                for (int j = -border; j <= border; j++)
                {
                    gx += g[x + i, y + j] * sx[i + border, j + border];
                    gy += g[x + i, y + j] * sx[j + border, i + border];
                }
            return Math.Sqrt(gx * gx + gy * gy);
        }
    }
}