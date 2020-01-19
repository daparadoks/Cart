namespace Paradox.Core
{
    public class Coupon : DiscountBase
    {
        public Coupon(double minimumAmount, double discount, DiscountType discountType): base(discount, discountType)
        {
            MinimumAmount = minimumAmount;
        }
        public double MinimumAmount { get;}
    }
}