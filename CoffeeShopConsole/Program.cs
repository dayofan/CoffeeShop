namespace CoffeeShopProblem
{
    using System;
    using CoffeeShop.Core;

    class Program
    {
        static void Main()
        {
            var customerManager = new CustomerManager();
            var coffeeShopManager = new CoffeeShopManager(customerManager);

            string command;
            do
            {
                command = Console.ReadLine() ?? "";
                coffeeShopManager.Run(command);
            } while (command != "exit");
        }

        
    }
}
