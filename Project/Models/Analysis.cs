namespace Project.Models
{
    public record Analysis
    {
        public string Category {  get; init; }
        public decimal BudgetLimit { get; init; }
        public decimal ActualSpent {  get; init; }
        public bool IsOverBudget { get; init; }
        public decimal Remaining {  get; init; }

        public Analysis(string categoryName, decimal limit, decimal actualSpent)
        {
            Category = categoryName;
            BudgetLimit = limit;
            ActualSpent = actualSpent;
            IsOverBudget = ActualSpent - limit > 0;
            Remaining = limit - actualSpent;
        }
    }
}