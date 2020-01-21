using System.Collections.Generic;

namespace Paradox.Core
{
    public class RateDiscountCalculator: IDiscountCalculator
    {
        public double Discount { get; set; }
        public double Price { get; set; }

        public double Calculate()
        {
            return MathHelper.Percentage(Price, (int) Discount);
        }
    }

    public class AmountDiscountCalculator : IDiscountCalculator
    {
        public double Discount { get; set; }
        public double Price { get; set; }

        public double Calculate()
        {
            return Discount;
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
            Calculators[direction].Price = price;
            Calculators[direction].Discount = discount;
            return Calculators[direction].Calculate();
        }
    }
}