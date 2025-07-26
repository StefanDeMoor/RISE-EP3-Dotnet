using Rise.Domain.Overviews;
using System.Text.Json.Serialization;

namespace Rise.Domain.AmountItems
{
    public class AmountItem
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public double Amount { get; set; }

        // Link to parent overview
        public int OverviewId { get; set; }
        [JsonIgnore]
        public Overview? Overview { get; set; } = default!;

        // Recursive relationship: parent and sub-amounts
        public int? ParentAmountItemId { get; set; }
        public AmountItem? ParentAmountItem { get; set; }

        public List<AmountItem> SubAmounts { get; set; } = new();
    }

}
