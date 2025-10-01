using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class UserManagerService : IUserManagerService
    {
        private ILimitManagerService _limitManagerService;
        private IRecommendationManagerService _recommendManagerService;

        private IUserManagerRepositorie _userManagerRepositorie;

        public UserManagerService
            (
            ILimitManagerService limitManagerService, 
            IRecommendationManagerService recommendManagerService,
            IUserManagerRepositorie userManagerRepositorie
            )
        {
            _limitManagerService = limitManagerService;
            _recommendManagerService = recommendManagerService;
            _userManagerRepositorie = userManagerRepositorie;
        }

        public void AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            if (CheckUserIdValidation(addTransactionRequest.UserId))
            {
                throw new Exception("Такого пользователя не существует");
            }
            User user = _userManagerRepositorie.Users.Find(x => x.UserId == addTransactionRequest.UserId);
            if (!user.Categories.Any(x => x.CategoryName == addTransactionRequest.СategoryName))
            {
                List<Transaction> transactions = [new Transaction(addTransactionRequest.Description, addTransactionRequest.Amount)];
                user.Categories.Add(new Category
                {
                    CategoryName = addTransactionRequest.СategoryName,
                    Transactions = transactions
                });
            }
            else
            {
                Category category = user.Categories.Find(x => x.CategoryName == addTransactionRequest.СategoryName);
                if (category.Limit + addTransactionRequest.Amount > category.Limit)
                {
                    throw new Exception("Вы не можете превысить лимит по этой категории");
                }
                category.Transactions.Add(new Transaction(addTransactionRequest.Description, addTransactionRequest.Amount));
            }
        }

        public List<Transaction> GetUserTransactions(int userId)
        {
            if (CheckUserIdValidation(userId))
            {
                throw new Exception("Такого пользователя не существует");
            }
            User user = _userManagerRepositorie.Users.Find(x => x.UserId == userId);
            List<Transaction> transactions = new List<Transaction>();
            foreach (var transactionList in user.Categories)
            {
                transactions.AddRange(transactions);
            }
            return transactions;
        }

        public void AddLimit(AddLimitRequest addLimitRequest)
        {
            _limitManagerService.AddLimit(addLimitRequest);
        }

        public List<Analysis> GetAnalyzes(int userId)
        {
            if (CheckUserIdValidation(userId))
            {
                throw new Exception("Такого пользователя не существует");
            }
            User user = _userManagerRepositorie.Users.Find(x => x.UserId == userId);
            List<Analysis> analysis = new List<Analysis>();
            int userCatigoriesCount = user.Categories.Count;
            for (int i = 0; i < userCatigoriesCount; i++)
            {
                string categoryName = user.Categories[i].CategoryName;
                decimal limit = user.Categories[i].Limit;
                decimal actualSpent = 0;
                int userCatigoriesTransactionsCount = user.Categories[i].Transactions.Count;
                for (int j = 0; j < userCatigoriesTransactionsCount; j++)
                {
                    actualSpent += user.Categories[i].Transactions[j].Amount;
                }
                analysis.Add(new Analysis(categoryName, limit, actualSpent));
            }
            return analysis;
        }

        private bool CheckUserIdValidation(int userId) => !_userManagerRepositorie.Users.Any(x => x.UserId == userId);
    }
}