namespace Project.Models
{
    public record Category
    {
        public string? CategoryName {  get; set; }
        public decimal Limit { get; set; } = 0;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}