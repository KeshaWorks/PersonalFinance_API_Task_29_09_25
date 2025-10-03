using Project.Models;

namespace Project.Interfaces
{
    public interface IUserManagerService
    {
        public void AddTransaction(AddTransactionRequest addTransactionRequest);
        public List<Transaction> GetUserTransactions(int userId);
        public void AddLimit(AddLimitRequest addLimitRequest);
        public List<Analysis> GetAnalyzes(int userId);
        public InsightSaving[] GetInsightsSavings(int userId);
    }
}