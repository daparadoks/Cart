namespace Paradox.Core
{
    public interface IDiscountCalculator
    {
        double Discount { get; set; }
        double Price { get; set; }
        
        double Calculate();
    }
}