using Project.Interfaces;
using Project.Models;
using Project.Models.TakedFromBody;
using Transaction = Project.Models.Transaction;

namespace Project.Services
{
    /// <summary>
    /// Main class for user's actions managment
    /// </summary>
    public class UserManagerService : IUserManagerService
    {
        private IUserManagerRepositorie _userManagerRepositorie;

        private readonly ILogger<UserManagerService> _logger;

        public UserManagerService
            (
            IUserManagerRepositorie userManagerRepositorie,
            ILogger<UserManagerService> logger
            )
        {
            _userManagerRepositorie = userManagerRepositorie;
            _logger = logger;
        }

        /// <summary>
        /// Add Transaction for specified user
        /// </summary>
        /// <param name="addTransactionRequest"></param>
        /// <exception cref="Exception">Limit validation</exception>
        public void AddTransaction(AddTransactionRequest addTransactionRequest)
        {
            var user = _userManagerRepositorie.GetUserById(addTransactionRequest.UserId);

            if (!user.Categories.Any(x => x.CategoryName == addTransactionRequest.СategoryName))
            {
                _logger.LogError("Лимит не обнаружен");
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
                _logger.LogError("Попытка превышения лимита");
                throw new Exception("Вы не можете превысить лимит по этой категории!");
            }

            category.Transactions.Add(new Transaction(addTransactionRequest.Description, addTransactionRequest.Amount));

            _logger.LogInformation("Записи успещно записаны");
        }

        /// <summary>
        /// Get User's Transactions in every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Transaction> GetUserTransactions(int userId)
        {
            var user = _userManagerRepositorie.GetUserById(userId);

            List<Transaction> transactions = new List<Transaction>();

            foreach (var transactionList in user.Categories)
            {
                transactions.AddRange(transactionList.Transactions);
            }

            _logger.LogInformation("Записи отправляются");

            return transactions;
        }

        /// <summary>
        /// Get information about every categorie
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Analysis> GetAnalyzes(int userId)
        {
            var user = _userManagerRepositorie.GetUserById(userId);
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

            _logger.LogInformation("Записи отправляются");

            return analysis;
        }
    }
}