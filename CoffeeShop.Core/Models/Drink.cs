namespace CoffeeShop.Core.Models
{
    public class Drink
    {
        public Drink(string name, decimal baseCost, decimal basePrice, int loyaltyPointsGained)
        {
            Name = name;
            BaseCost = baseCost;
            BasePrice = basePrice;
            LoyaltyPointsGained = loyaltyPointsGained;
        }

        public string Name { get; }
        public decimal BaseCost { get; }
        public decimal BasePrice { get; }
        public int LoyaltyPointsGained { get; }
    }
}
