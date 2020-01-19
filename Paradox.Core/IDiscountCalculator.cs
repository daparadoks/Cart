namespace Paradox.Core
{
    public interface IDiscountCalculator
    {
        double Calculate(double discount, double price);
    }
}