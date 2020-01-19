using System.Collections.Generic;

namespace Paradox.Core
{
    public class RateDiscountCalculator: IDiscountCalculator
    {
        public double Calculate(double price, double discount)
        {
            return MathHelper.Percentage(price, (int) discount);
        }
    }

    public class AmountDiscountCalculator : IDiscountCalculator
    {
        public double Calculate(double price, double discount)
        {
            return discount;
        }
    }

    public static class DiscountCalculator
    {
        private static readonly Dictionary<DiscountType, IDiscountCalculator> Calculators = new Dictionary<DiscountType, IDiscountCalculator>();
 
        static DiscountCalculator()
        {
            Calculators.Add(DiscountType.Rate, new RateDiscountCalculator());
            Calculators.Add(DiscountType.Amount, new AmountDiscountCalculator());
        }
        public static double Calculate(DiscountType direction, double price, double discount)
        {
            return Calculators[direction].Calculate(price, discount);
        }
    }
}