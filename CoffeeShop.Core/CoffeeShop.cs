using System;
using System.Linq;
using System.Collections.Generic;
using CoffeeShop.Core.Models.Customer;
using CoffeeShop.Core.Models;
using System.Collections.ObjectModel;

namespace CoffeeShop.Core
{

    public class CoffeeShop
    {
        private const int BEANS_PER_CUP = 150;
        private int _coffeeBeansCount = 1000;
        private decimal _profit = 0;
        private decimal _costOfDrinks = 0;
        private decimal _incomeFromDrinks = 0;
        private int _totalLoyaltyPointsAccrued = 0;
        private int _totalLoyaltyPointsRedeemed = 0;
        private int _totalCupsSold = 0;
        private List<Customer> _customers;

        public CoffeeShop()
        {
            _customers = new List<Customer>();
        }

        //public Drink Drink { get; private set; }
        public ReadOnlyCollection<Customer> Customers => new ReadOnlyCollection<Customer>(_customers);

        public void AddCustomer(Customer customer) => _customers.Add(customer);

        public void AddDrinkToCustomerOrder(Guid id, Drink drink)
        {
            var customer = _customers.Where(x => x.Id == id).FirstOrDefault();
            customer?.AddDrink(drink);
        }

        public string GetSummary()
        {

            int totalCustomers = Customers.Count;

            string result = "Coffee Shop Summary";

            foreach (var customer in Customers)
            {
                switch (customer.Type)
                {
                    case CustomerType.LoyaltyMember:
                        if (_coffeeBeansCount < BEANS_PER_CUP) break;
                        var loyaltyMember = customer as LoyaltyMember;
                        UpdateBasedOnLoyaltyPoints(loyaltyMember);
                        UpdateShopAccount(loyaltyMember);
                        break;
                    case CustomerType.Employee:
                        if (_coffeeBeansCount < BEANS_PER_CUP) break;
                        var employee = customer as Employee;
                        UpdateShopAccount(employee);
                        if (!employee.CanRedeemFreeDrink() || _profit >= 50)
                            _incomeFromDrinks += employee.DrinksPurchased[0].BasePrice;
                        break;
                    default:
                        if (_coffeeBeansCount < BEANS_PER_CUP) break;
                        UpdateShopAccount(customer);
                        _incomeFromDrinks += customer.DrinksPurchased[0].BasePrice;
                        break;
                }
            }

            //var profit = incomeFromDrinks - costOfDrinks;
            //costOfDrinks = Drink.BaseCost * totalCupsSold;
            result += Constants.VerticalWhiteSpace;
            result += "Total customers: " + totalCustomers;
            result += Constants.NewLine;
            result += Constants.Indentation + "General sales: " + Customers.Count(p => p.Type == CustomerType.General);
            result += Constants.NewLine;
            result += Constants.Indentation + "Loyalty member sales: " + Customers.Count(p => p.Type == CustomerType.LoyaltyMember);
            result += Constants.NewLine;
            result += Constants.Indentation + "Employee Complimentary: " + Customers.Count(p => p.Type == CustomerType.Employee);

            result += Constants.VerticalWhiteSpace;

            result += "Total revenue from drinks: " + _incomeFromDrinks;
            result += Constants.NewLine;
            result += "Total costs from drinks: " + _costOfDrinks;
            result += Constants.NewLine;


            result += (_profit > 0 ? "Coffee Shop generating profit of: " : "Coffee Shop losing money of: ") + _profit;

            result += Constants.VerticalWhiteSpace;

            result += "Total loyalty points given away: " + _totalLoyaltyPointsAccrued;
            result += Constants.NewLine;
            result += "Total loyalty points redeemed: " + _totalLoyaltyPointsRedeemed;
            result += Constants.NewLine;
            result += "Total coffee beans left: " + _coffeeBeansCount;
            result += Constants.VerticalWhiteSpace;

            var shopCanOpenTomorrow = (_profit > 20 && _coffeeBeansCount >= 250);
            result += shopCanOpenTomorrow ? "Coffee Shop will open tomorrow " : "Coffee Shop will not open tomorrow";

            return result;
        }

        private void UpdateBasedOnLoyaltyPoints(LoyaltyMember loyaltyMember)
        {
            bool loyaltyPointsUpdateSuccessful = false;
            if (loyaltyMember.IsUsingLoyaltyPoints)
            {
                int loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(loyaltyMember.DrinksPurchased[0].BasePrice));
                loyaltyPointsUpdateSuccessful = loyaltyMember.AddLoyaltyPoints(-loyaltyPointsRedeemed);

                if (loyaltyPointsUpdateSuccessful)
                    _totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
            }
            if (!loyaltyMember.IsUsingLoyaltyPoints || !loyaltyPointsUpdateSuccessful)
            {
                _totalLoyaltyPointsAccrued += loyaltyMember.DrinksPurchased[0].LoyaltyPointsGained;
                _incomeFromDrinks += loyaltyMember.DrinksPurchased[0].BasePrice;
            }
        }

        private void UpdateShopAccount(Customer customer)
        {
            _costOfDrinks += customer.DrinksPurchased[0].BaseCost;
            _coffeeBeansCount -= BEANS_PER_CUP;
            _profit += _incomeFromDrinks - _costOfDrinks;
            _totalCupsSold++;
        }
    }
}
