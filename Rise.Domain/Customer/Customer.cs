namespace Rise.Domain.Customer
{
    public class Customer
    {
        public int Id { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
