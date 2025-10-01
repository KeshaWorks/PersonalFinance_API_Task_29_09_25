namespace Project.Models
{
    public record Analysis
    {
        public string Category {  get; init; }
        public decimal BudgetLimit { get; init; }
        public decimal ActualSpent {  get; init; }

        private bool _isOverBudget;
        private decimal _remaining;

        public Analysis(string categoryName, decimal limit, decimal actualSpent)
        {
            Category = categoryName;
            BudgetLimit = limit;
            ActualSpent = actualSpent;
            _isOverBudget = ActualSpent - limit > 0;
            _remaining = limit - actualSpent;
        }
    }
}