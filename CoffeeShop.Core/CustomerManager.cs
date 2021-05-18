using CoffeeShop.Core.Models.Customer;
using System;

namespace CoffeeShop.Core
{
    public class CustomerManager : ICustomerManager
    {
        public Customer SetupCustomer(string enteredText, CustomerType customerType)
        {
            var segments = enteredText.Split(' ');
            string name = segments[2];
            var customer = CreateCustomer(name, customerType);
            if (customer is LoyaltyMember loyaltyMember)
            {
                var loyaltyPoints = Convert.ToInt32(segments[3]);
                var isUsingLoyaltyPoints = Convert.ToBoolean(segments[4]);

                loyaltyMember.AddLoyaltyPoints(loyaltyPoints);
                loyaltyMember.ShouldUseLoyaltyPoints(isUsingLoyaltyPoints);

                return loyaltyMember;
            }
            return customer;
        }

        private Customer CreateCustomer(string name, CustomerType customerType)
        {
            Customer customer;
            switch (customerType)
            {
                case CustomerType.General:
                    customer = General.Create(name);
                    break;
                case CustomerType.LoyaltyMember:
                    customer = LoyaltyMember.Create(name);
                    break;
                case CustomerType.Employee:
                    customer = Employee.Create(name);
                    break;
                case CustomerType.DiscountedCustomer:
                    customer = DiscountedCustomer.Create(name);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return customer;
        }
    }
}
