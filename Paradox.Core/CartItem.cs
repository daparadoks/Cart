namespace Paradox.Core
{
    public class CartItem
    {
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity > 0 ? quantity : 1;
        }
        
        public Product Product { get;}
        public int Quantity { get;}

        public double Price => Quantity * Product.Price;
    }
}