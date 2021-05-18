namespace CoffeeShop.Core.Models.Customer
{
    public class Employee : Customer
    {
        private int _freeDrinksCount = 2;

        private Employee(string name) : base(name)
        {
        }

        public int FreeDrinksCount { get => _freeDrinksCount; }
        public override CustomerType Type => CustomerType.Employee;

        public bool CanRedeemFreeDrink() => _freeDrinksCount > 0;
        public void RedeemFreeDrink()
        {
            if (!CanRedeemFreeDrink()) return;
            _freeDrinksCount--;
        }

        public static Employee Create(string name) => new Employee(name);
    }
}
