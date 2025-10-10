namespace Project.Models.TakedFromBody
{
    /// <summary>
    /// Record that gets information about transaction
    /// </summary>
    public record AddTransactionRequest
    {
        public int UserId { get; init; }
        public string Description { get; init; }
        public decimal Amount { get; set; }
        public string СategoryName { get; init; }
    }
}