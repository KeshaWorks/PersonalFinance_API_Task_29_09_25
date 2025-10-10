using Project.Models;
using Project.Models.TakedFromBody;

namespace Project.Interfaces
{
    public interface IUserManagerService
    {
        public void AddTransaction(AddTransactionRequest addTransactionRequest);
        public List<Transaction> GetUserTransactions(int userId);
        public List<Analysis> GetAnalyzes(int userId);
    }
}