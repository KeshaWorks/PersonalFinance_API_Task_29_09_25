namespace Project.Models
{
    public record Transaction
    {
        public string? Description { get; init; }
        public decimal Amount { get; init; }

        public Transaction(string description, decimal amount)
        {
            Description = description;
            Amount = amount;
        }
    }
}