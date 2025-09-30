namespace Project.Models
{
    public record Transaction
    {
        public string? Description { get; init; }
        public decimal Amount { get; init; }
    }
}