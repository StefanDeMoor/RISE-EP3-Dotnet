using Ardalis.GuardClauses;
using Rise.Domain.Common;
using System;

namespace Rise.Domain.Categories
{
    public class Category : Entity
    {
        private string name = default!;

        public required string Name
        {
            get => name;
            set => name = Guard.Against.NullOrWhiteSpace(value);
        }
    }

}