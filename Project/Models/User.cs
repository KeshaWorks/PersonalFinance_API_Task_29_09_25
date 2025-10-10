namespace Project.Models
{
    public record User
    {
        public int UserId { get; init; } 
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}