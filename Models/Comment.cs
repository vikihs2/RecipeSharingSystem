namespace RecipeSharingSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}