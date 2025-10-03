namespace Project.Models
{
    public record Category
    {
        public string? CategoryName {  get; init; }
        public decimal Limit { get; set; }
        public List<Transaction> Transactions { get; init; } = new List<Transaction>();
    }
}