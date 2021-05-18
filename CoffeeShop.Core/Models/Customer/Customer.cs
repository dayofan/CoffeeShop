using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoffeeShop.Core.Models.Customer
{
    public abstract class Customer
    {
        private readonly List<Drink> _drinksPurchased;

        public Customer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            Name = name;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string Name { get; }
        public ReadOnlyCollection<Drink> DrinksPurchased => new ReadOnlyCollection<Drink>(_drinksPurchased);
        public abstract CustomerType Type { get; }

        public void AddDrink(Drink drink) => _drinksPurchased.Add(drink);
    }
}
