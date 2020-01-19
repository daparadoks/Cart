using System.Collections.Generic;
using System.Linq;

namespace Paradox.Core
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }
        
        public IList<CartItem> Items { get; private set; }
        public double TotalPrice { get; private set; }
        public double Discount { get; private set; }
        public double CouponAmount { get; private set; }
        public double DeliveryCost { get; private set; }
        public double AmountToBePaid { get; private set; }
        public double DiscountTotal => Discount + CouponAmount;
        
        public void ApplyCampaign(List<Campaign> campaignList)
        {
            var suitableCampaigns = campaignList?.Where(x => IsSuitable(Items, x)).ToList();
            if((suitableCampaigns?.Count ?? 0) <= 0)
                return;
            
            var campaign = suitableCampaigns.OrderByDescending(x => GetDiscountAmount(Items, x)).FirstOrDefault();
            Discount = GetDiscountAmount(Items, campaign);
        }

        private void SetTotalPrice()
        {
            TotalPrice =  Items.Sum(x => x.Price);
        }

        public void SetFinalPrice()
        {
            AmountToBePaid = TotalPrice - Discount - CouponAmount + DeliveryCost;
        }

        public void ApplyCoupon(Coupon coupon)
        {
            var basePrice = TotalPrice - Discount;
            CouponAmount = basePrice >= coupon?.MinimumAmount
                ? DiscountCalculator.Calculate(coupon.DiscountType, basePrice, coupon.Discount)
                : 0;
        }

        public void SetDeliveryCost()
        {
            var deliveryCount = GetDeliveryCount();
            var itemCount = Items.Sum(i => i.Quantity);
            DeliveryCost = MathHelper.Round((DeliveryConstants.CostPerDelivery * deliveryCount) +
                           (DeliveryConstants.CostPerProduct * itemCount) +
                           DeliveryConstants.FixedCost);
        }

        public void AddItem(Product product, int quantity)
        {
            if (product.Stock < quantity)
                return;

            Items.Add(new CartItem(product, quantity));
            SetTotalPrice();
        }
        
        private int GetDeliveryCount()
        {
            return Items.GroupBy(i => i.Product.Definition.Category).Select(s => s.Key).Count();
        }

        public List<string> Print()
        {
            var messages = new List<string>();
            foreach (var cartItem in Items)
            {
                messages.Add($"Kategori: {cartItem.Product.Definition.Category.Title}, ürün: {cartItem.Product.Definition.Title}, adet: {cartItem.Quantity}, birim fiyat: {cartItem.Product.Price}");
            }

            messages.Add($"Toplam Tutar: {TotalPrice}");
            messages.Add($"İndirimler: {DiscountTotal}");
            messages.Add($"Kargo Ücreti: {DeliveryCost}");
            messages.Add($"Ödenecek Tutar: {AmountToBePaid}");

            return messages;
        }
        
        private double GetDiscountAmount(IList<CartItem> items, Campaign campaign)
        {
            var suitableItems = GetSuitableItems(items, campaign);
            var price = suitableItems.Sum(x => x.Price);
            return DiscountCalculator.Calculate(campaign.DiscountType, price, campaign.Discount);
        }

        private IList<CartItem> GetSuitableItems(IList<CartItem> items, Campaign campaign)
        {
            var suitableItems = items?.Where(i => campaign?.Category == null || i.Product.Definition.Category.Id == campaign.Category.Id)
                .ToList();

            return suitableItems;
        }

        private bool IsSuitable(IList<CartItem> items, Campaign campaign)
        {
            var suitableItems = GetSuitableItems(items, campaign);
            return suitableItems.Sum(x => x.Quantity) >= campaign.MinimumItem;
        }
    }
}