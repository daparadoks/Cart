using System;

namespace Paradox.Core
{
    public static class MathHelper
    {
        public static double Percentage(double sum, int percent)
        {
            return Round(sum / 100 * percent);
        }

        public static double Round(double value)
        {
            return Math.Round(value, 2);
        }
    }
}