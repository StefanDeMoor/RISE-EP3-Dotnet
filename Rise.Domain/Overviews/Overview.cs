using Rise.Domain.Categories;
using Rise.Domain.AmountItems;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Rise.Domain.Overviews
{
    public class Overview
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        [BindNever] 
        public Category? Category { get; set; } = default!;

        public double? TotalIncome { get; set; }
        public double Result { get; set; }

        public List<AmountItem>? Amounts { get; set; } = new();
    }

}
