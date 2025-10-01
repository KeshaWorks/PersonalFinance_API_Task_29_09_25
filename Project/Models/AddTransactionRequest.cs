namespace Project.Models
{
    public record AddTransactionRequest
    {
        public int UserId {  get; init; }
        public string Description { get; init; }
        public decimal Amount { get; set; }
        public string СategoryName {  get; init; }

        public AddTransactionRequest(int userId, string description, decimal amount, string category)
        {
            UserId = userId;
            if (description is null)
            {
                throw new ArgumentNullException("description cannot be null");
            }
            Description = description;
            if (amount == 0)
            {
                throw new FormatException("amount cannot be null");
            }
            Amount = amount;
            if (category is null)
            {
                throw new ArgumentNullException("category cannot be null");
            }
            СategoryName = category;
        }
    }
}