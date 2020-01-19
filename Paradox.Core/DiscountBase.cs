namespace Paradox.Core
{
    public class DiscountBase : Base
    {
        protected DiscountBase(double discount, DiscountType type)
        {
            Discount = discount;
            DiscountType = type;
        }

        public double Discount { get; }
        public DiscountType DiscountType { get; }
    }
}