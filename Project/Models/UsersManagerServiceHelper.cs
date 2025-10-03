namespace Project.Models
{
    /// <summary>
    /// Broker that help UsersManagerService
    /// </summary>
    public record UsersManagerServiceHelper
    {
        public decimal Limit { get; init; }
        public decimal TotalSum {  get; init; }
        public string? Categorie { get; init; }

        public UsersManagerServiceHelper(decimal limit, decimal totalSym, string? categorie)
        {
            Limit = limit;
            TotalSum = totalSym;
            Categorie = categorie;
        }
    }
}