using System.ComponentModel.DataAnnotations;

namespace RecipeSharingSystem.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? AuthorUsername { get; set; }

        public string CreatedBy { get; set; }
    }
}