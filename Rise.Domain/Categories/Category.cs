using Ardalis.GuardClauses;
using Rise.Domain.Overviews;
using System.Text.Json.Serialization;

namespace Rise.Domain.Categories
{
    public class Category
    {

        public int Id { get; set; }
        private string name = default!;

        public required string Name
        {
            get => name;
            set => name = Guard.Against.NullOrWhiteSpace(value);
        }

        [JsonIgnore]
        public List<Overview>? Overviews { get; set; } = new();
    }

}