namespace Paradox.Core
{
    public class Product : Base
    {
        public Product(ProductDefinition definition, double price, int stock)
        {
            Definition = definition;
            Price = price;
            Stock = stock;
        }
        public ProductDefinition Definition { get; }
        public double Price { get; }
        public int Stock { get; }
    }
}