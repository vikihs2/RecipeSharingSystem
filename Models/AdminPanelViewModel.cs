using System.Collections.Generic;

namespace RecipeSharingSystem.Models
{
    public class AdminPanelViewModel
    {
        public List<Recipe> PendingRecipes { get; set; } = new List<Recipe>();
        public List<Recipe> AllRecipes { get; set; } = new List<Recipe>();
        public List<int> FeaturedRecipeIds { get; set; } = new List<int>();
        public int TotalUsers { get; set; }
        public int TotalRecipes { get; set; }
        public TopContributorViewModel TopContributor { get; set; }
    }

    public class TopContributorViewModel
    {
        public string UserName { get; set; }
        public int RecipeCount { get; set; }
    }
}