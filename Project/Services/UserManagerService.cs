using Project.Interfaces;
using Project.Models;
using Transaction = Project.Models.Transaction;

namespace Project.Services
{
    /// <summary>
    /// Main class for user's actions managment
    /// </summary>
    public class UserManagerService : IUserManagerService
    {
        public List<User> Users { get; set; } =
        [
            new User { UserId = 1, Categories = new List<Category>()},
            new User { UserId = 2, Categories = new List<Category>()},
            new User { UserId = 3, Categories = new List<Category>()},
        ];

        private ILimitManagerService _limitManagerService;
        private IRecommendationManagerService _recommendManagerService;

        public UserManagerService
            (
            ILimitManagerService limitManagerService, 
            IRecommendationManagerService recommendManagerService
            )
        {
            _limitManagerService = limitManagerService;
            _recommendManagerService = recommendManagerService;
        }

        /// <summary>
        /// Add Transaction for specified user
        /// </summary>
        /// <param name="addTransactionRequest"></param>
        /// <exception cref="Exception">Limit validation</exception>
        public void AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            User user = Users.Find(x => x.UserId == addTransactionRequest.UserId);

            if (!user.Categories.Any(x => x.CategoryName == addTransactionRequest.СategoryName))
            {
                throw new Exception("Сначала установаите лимит для этой категории!");
            }

            Category category = user.Categories.Find(x => x.CategoryName == addTransactionRequest.СategoryName);
            decimal categorySum = 0;

            foreach (var transactionList in category.Transactions)
            {
                categorySum += transactionList.Amount;
            }

            if (categorySum + addTransactionRequest.Amount > category.Limit)
            {
                throw new Exception("Вы не можете превысить лимит по этой категории!");
            }

            category.Transactions.Add(new Transaction(addTransactionRequest.Description, addTransactionRequest.Amount));
        }

        /// <summary>
        /// Get User's Transactions in every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Transaction> GetUserTransactions(int userId)
        {
            User user = Users.Find(x => x.UserId == userId);
            List<Transaction> transactions = new List<Transaction>();

            foreach (var transactionList in user.Categories)
            {
                transactions.AddRange(transactionList.Transactions);
            }

            return transactions;
        }

        /// <summary>
        /// Add limit for specified categorie
        /// </summary>
        /// <param name="addLimitRequest"></param>
        public void AddLimit(AddLimitRequest addLimitRequest) => _limitManagerService.AddLimit(addLimitRequest, Users);

        /// <summary>
        /// Get information about every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Analysis> GetAnalyzes(int userId)
        {
            User user = Users.Find(x => x.UserId == userId);
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

        /// <summary>
        /// Get 3 recommendation about expenses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public InsightSaving[] GetInsightsSavings(int userId) => _recommendManagerService.GetInsightsSavings(userId, Users);
    }
}