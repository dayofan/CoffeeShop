namespace CoffeeShop.Core.Models.Customer
{
    public class LoyaltyMember : Customer
    {
        private int _loyaltyPoints;
        private bool _isUsingLoyaltyPoints;

        private LoyaltyMember(string name) : base(name)
        {
        }

        public int LoyaltyPoints
        {
            get => _loyaltyPoints;
            private set => _loyaltyPoints = value;
        }
        public bool IsUsingLoyaltyPoints
        {
            get => _isUsingLoyaltyPoints;
            private set => _isUsingLoyaltyPoints = value;
        }
        public override CustomerType Type => CustomerType.LoyaltyMember;
        /// <summary>
        /// Adds Loyalty points and returns whether the operation is successful.
        /// </summary>
        /// <param name="loyaltyPoints">Loyalty points to be added.</param>
        /// <returns>Whether the update was successful or not.</returns>
        public bool AddLoyaltyPoints(int loyaltyPoints)
        {
            var potentialCummulativePoints = _loyaltyPoints + loyaltyPoints;
            if (potentialCummulativePoints < 0) return false;
            _loyaltyPoints = potentialCummulativePoints;
            return true;
        }

        public void ShouldUseLoyaltyPoints(bool shouldUseLoyaltyPoints) => _isUsingLoyaltyPoints = shouldUseLoyaltyPoints;

        public static LoyaltyMember Create(string name) => new LoyaltyMember(name);
    }
}
