namespace Project.Models
{
    public class User
    {
        public int UserId { get; init; }
        public decimal Finance { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}