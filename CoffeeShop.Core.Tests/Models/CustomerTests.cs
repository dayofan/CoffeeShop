using CoffeeShop.Core.Models.Customer;
using NUnit.Framework;
using System;

namespace CoffeeShop.Core.Tests.Models
{
    public class CustomerTests
    {
        [TestCase(typeof(General), CustomerType.General)]
        [TestCase(typeof(Employee), CustomerType.Employee)]
        [TestCase(typeof(LoyaltyMember), CustomerType.LoyaltyMember)]
        [TestCase(typeof(DiscountedCustomer), CustomerType.DiscountedCustomer)]
        public void WhenCreated_CustomerTypeIsCorrect(Type type, CustomerType customerType) =>
            Assert.AreEqual(type.Name, customerType.ToString());

        [Test]
        public void AddLoyaltyPoints_LoyaltyPointsAdded()
        {
            // Arrange
            var loyaltyMember = LoyaltyMember.Create("customer");

            // Act
            loyaltyMember.AddLoyaltyPoints(5);
            loyaltyMember.AddLoyaltyPoints(10);
            loyaltyMember.AddLoyaltyPoints(-5);
            loyaltyMember.AddLoyaltyPoints(-15);

            // Assert
            Assert.AreEqual(10, loyaltyMember.LoyaltyPoints);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetUsingLoyaltyPoints_Sets(bool useLoyaltyPoints)
        {
            // Arrange
            var loyaltyMember = LoyaltyMember.Create("customer");

            // Act
            loyaltyMember.ShouldUseLoyaltyPoints(useLoyaltyPoints);

            // Assert
            Assert.AreEqual(useLoyaltyPoints, loyaltyMember.IsUsingLoyaltyPoints);
        }
    }
}
