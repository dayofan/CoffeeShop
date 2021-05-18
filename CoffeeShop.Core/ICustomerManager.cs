using CoffeeShop.Core.Models.Customer;

namespace CoffeeShop.Core
{
    public interface ICustomerManager
    {
        Customer SetupCustomer(string enteredText, CustomerType customerType);
    }
}