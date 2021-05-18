using CoffeeShop.Core.Models;
using CoffeeShop.Core.Models.Customer;
using System;

namespace CoffeeShop.Core
{
    public class CoffeeShopManager
    {
        private readonly CoffeeShop _coffeeShop;
        private readonly ICustomerManager _customerManager;

        public CoffeeShopManager(ICustomerManager customerManager)
        {
            _customerManager = customerManager ?? throw new ArgumentNullException(nameof(customerManager));
            _coffeeShop = new CoffeeShop();
        }

        public void Run(string command)
        {
            var enteredText = command.ToLower();

            if (enteredText.Contains("print summary"))
            {
                Console.WriteLine();
                Console.WriteLine(_coffeeShop.GetSummary());
            }
            else if (enteredText.Contains("add general"))
            {
                SetupAndAddCustomer(enteredText, CustomerType.General);

            }
            else if (enteredText.Contains("add loyalty"))
            {
                SetupAndAddCustomer(enteredText, CustomerType.LoyaltyMember);

            }
            else if (enteredText.Contains("add employee"))
            {
                SetupAndAddCustomer(enteredText, CustomerType.Employee);
            }
            else if (enteredText.Contains("exit"))
            {
                Environment.Exit(1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UNKNOWN INPUT");
                Console.ResetColor();
            }
        }

        private void SetupAndAddCustomer(string enteredText, CustomerType customerType)
        {
            var drink = new Drink("Americano", 50, 100, 5);
            var segments = enteredText.Split(' ');
            var name = segments[2];
            var customer = _customerManager.SetupCustomer(name, customerType);

            _coffeeShop.AddCustomer(customer);
            _coffeeShop.AddDrinkToCustomerOrder(customer.Id, drink);
        }
    }
}
