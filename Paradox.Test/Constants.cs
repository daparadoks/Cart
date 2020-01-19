using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Paradox.Core;

namespace Paradox.Cart.Test
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        private static class ProductDefinitionConstants
        {
            public static ProductDefinition Cpu => new ProductDefinition("Amd Ryzen 5 3600x", CategoryConstants.Hardware);
            public static ProductDefinition MainBoard => new ProductDefinition("Asus B450", CategoryConstants.Hardware);
            public static ProductDefinition Ram => new ProductDefinition("DD4 Ram", CategoryConstants.Hardware);
            
            public static ProductDefinition Headset => new ProductDefinition("Sennheiser S500", CategoryConstants.Electronic);
            
            public static ProductDefinition Jean => new ProductDefinition("Jean", CategoryConstants.Jean);
        }

        public static class ProductConstants
        {
            public static Product Cpu => new Product(ProductDefinitionConstants.Cpu, 1500, 5);
            public static Product MainBoard => new Product(ProductDefinitionConstants.MainBoard, 500, 2);
            public static Product OtherMainBoard => new Product(ProductDefinitionConstants.MainBoard, 500, 2);
            public static Product Ram => new Product(ProductDefinitionConstants.Ram, 382.45, 1);
            public static Product OtherRam => new Product(ProductDefinitionConstants.Ram, 385, 15);
            public static Product Headset => new Product(ProductDefinitionConstants.Headset, 499.99, 20);
            public static Product OtherHeadset => new Product(ProductDefinitionConstants.Headset, 485.5, 3);
            public static Product Jean => new Product(ProductDefinitionConstants.Jean, 200, 50);
        }
        
        public static class CategoryConstants
        {
            public static Category Hardware=> new Category("Bilgisayar Bileşenleri", 1);
            public static Category Electronic => new Category("Elektronik", 2);
            public static Category Jean => new Category("Pantolon", 3);
        }

        public static class CampaignConstants
        {
            public static List<Campaign> All => new List<Campaign>
            {
                WeekendCampaign20,
                WeekendCampaign50,
                WeekendCampaign5Tl
            };

            private static Campaign WeekendCampaign20 => new Campaign(CategoryConstants.Hardware, DiscountType.Rate, 3, 20);
            private static Campaign WeekendCampaign50 => new Campaign(CategoryConstants.Hardware, DiscountType.Rate, 5, 50);
            private static Campaign WeekendCampaign5Tl => new Campaign(CategoryConstants.Hardware, DiscountType.Amount, 5, 5);
        }

        public static class CouponConstants
        {
            public static Coupon WeekendCoupon => new Coupon(100, 10, DiscountType.Rate);
        }
    }
}