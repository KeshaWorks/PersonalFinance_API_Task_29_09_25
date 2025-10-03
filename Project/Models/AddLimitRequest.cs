using System.Diagnostics.CodeAnalysis;

namespace Project.Models
{
    /// <summary>
    /// Record that gets information about limit
    /// </summary>
    public record AddLimitRequest
    {
        public int UserId { get; init; }
        [NotNull]
        public string СategoryName { get; init; }
        public decimal Limit { get; init; }
    }
}