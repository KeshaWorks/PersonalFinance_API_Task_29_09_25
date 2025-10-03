namespace Project.Models
{
    public record InsightSaving
    {
        public string? Message { get; init; }
        public decimal PotentialSavings {  get; init; }
        public string? Category { get; init; }

        public InsightSaving(string? message, decimal potentialSavings, string category) 
        {
            Message = message;
            PotentialSavings = potentialSavings;
            Category = category;
        }
    }
}