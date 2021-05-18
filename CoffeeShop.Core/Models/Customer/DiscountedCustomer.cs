using System;

namespace CoffeeShop.Core.Models.Customer
{
    public class DiscountedCustomer : Customer
    {
        private readonly decimal _discount = 0.25m;
        private DiscountedCustomer(string name) : base(name)
        {
        }

        public override CustomerType Type => CustomerType.DiscountedCustomer;

        public decimal GetDiscountedPrice(decimal price) => price * (1-_discount);
        public static Customer Create(string name) => new DiscountedCustomer(name);
    }
}
