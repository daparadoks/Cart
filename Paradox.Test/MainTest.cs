using System;
using System.Collections.Generic;
using System.Linq;
using Paradox.Core;
using Xunit;
using Xunit.Abstractions;

namespace Paradox.Cart.Test
{
    public class MainTest: TestBase
    {

        private List<KeyValuePair<Product, int>> Products => new List<KeyValuePair<Product, int>>
        {
            new KeyValuePair<Product, int>(Constants.ProductConstants.Cpu, 1),
            new KeyValuePair<Product, int>(Constants.ProductConstants.MainBoard, 1),
            new KeyValuePair<Product, int>(Constants.ProductConstants.Ram, 1),
            new KeyValuePair<Product, int>(Constants.ProductConstants.OtherRam, 1)
        };

        public MainTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
        }

        [Fact]
        public void TestAll()
        {
            var shoppingCart = new ShoppingCart();
            foreach (var productQuantityPair in Products)
                shoppingCart.AddItem(productQuantityPair.Key, productQuantityPair.Value);
            
            Assert.Equal(shoppingCart.Items.Count, Products.Count);
            Assert.Equal(shoppingCart.Items.Sum(x => x.Quantity), Products.Sum(x => x.Value));
            Assert.True(shoppingCart.TotalPrice > 0);
            
            shoppingCart.ApplyCampaign(Constants.CampaignConstants.All);
            Assert.True(shoppingCart.Discount > 0);
            
            shoppingCart.ApplyCoupon(Constants.CouponConstants.WeekendCoupon);
            Assert.True(shoppingCart.CouponAmount > 0);
            
            shoppingCart.SetDeliveryCost();
            Assert.True(shoppingCart.DeliveryCost > 0);
            
            shoppingCart.SetFinalPrice();
            Assert.True(shoppingCart.AmountToBePaid > 0);
            
            Print(shoppingCart.Print());
        }
        
        [Fact]
        public void ShouldAddProductsOverStock()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 100, 1), 2);
            Assert.Equal(0, shoppingCart.Items.Count);
            Assert.Equal(0, shoppingCart.TotalPrice);
        }
        
        [Fact]
        public void ShouldAddProducts()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 100, 50), 1);
            Assert.Equal(1, shoppingCart.Items.Count);
            foreach (var item in shoppingCart.Items)
            {
                Assert.Equal(1, item.Quantity);
                Assert.Equal(100, item.Price);
                Assert.Equal(100, item.Product.Price);
                Assert.Equal(50, item.Product.Stock);
                Assert.True(item.Product.Definition.Title == "Test");
                Assert.True(item.Product.Definition.Category.Title == "Kategori");
            }
        }
        
        [Fact]
        public void ShouldAddProductsZeroQuantity()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 100, 50), 1);
            Assert.Equal(1, shoppingCart.Items.Count);
            Assert.Equal(100, shoppingCart.TotalPrice);
        }
        
        [Fact]
        public void ShouldSetTotalPrice()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 100, 50), 1);
            Assert.Equal(100, shoppingCart.TotalPrice);
        }

        [Fact]
        public void ShouldApplyCampaign()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 100, 50), 1);
            shoppingCart.ApplyCampaign(new List<Campaign>
                {new Campaign(new Category("Kategori", 1), DiscountType.Amount, 1, 10)});
            Assert.Equal(10, shoppingCart.Discount);
        }
        
        [Fact]
        public void ShouldApplyCampaignWithNullData()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.ApplyCampaign(null);
            Assert.Equal(0, shoppingCart.Discount);
        }

        [Fact]
        public void ShouldApplyCoupon()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Genel", 1)), 500, 10), 1);
            shoppingCart.ApplyCoupon(new Coupon(100, 10, DiscountType.Rate));
            Assert.Equal(500, shoppingCart.TotalPrice);
            Assert.Equal(50, shoppingCart.CouponAmount);
        }

        [Fact]
        public void ShouldApplyDeliveryCost()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Genel", 1)), 500, 10), 1);
            Assert.Equal(500, shoppingCart.TotalPrice);
            shoppingCart.SetDeliveryCost();
            Assert.True(shoppingCart.DeliveryCost > 0);
        }
        
        [Fact]
        public void ShouldSetFinalPrice()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Genel", 1)), 500, 10), 1);
            shoppingCart.SetDeliveryCost();
            Assert.True(shoppingCart.DeliveryCost > 0);
            shoppingCart.SetFinalPrice();
            Assert.Equal(508, Math.Round(shoppingCart.AmountToBePaid));
        }

        [Fact]
        public void Should_Handle_Null_DiscountType()
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddItem(new Product(new ProductDefinition("Test", new Category("Kategori", 1)), 0, 50), 1);
            shoppingCart.ApplyCampaign(new List<Campaign>
                {new Campaign(new Category("Kategori", 1), DiscountType.Rate, 0, 0)});
            Assert.Equal(0, shoppingCart.Discount);
        }
    }
}