namespace CoffeeShop.Core.Models.Customer
{
    public class General : Customer
    {
        private General(string name) : base(name)
        {
        }

        public override CustomerType Type => CustomerType.General;

        public static General Create(string name) => new General(name);
    }
}
