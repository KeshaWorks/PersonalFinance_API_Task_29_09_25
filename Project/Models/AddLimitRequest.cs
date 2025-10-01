namespace Project.Models
{
    public record AddLimitRequest
    {
        public int UserId { get; init; }
        public string СategoryName { get; init; }
        public decimal Limit { get; init; }

        public AddLimitRequest(int uesrId, string category, decimal limit)
        {
            UserId = uesrId;
            if (category is null)
            {
                throw new ArgumentNullException("category cannot be null");
            }
            СategoryName = category;
            Limit = limit;
        }
    }
}