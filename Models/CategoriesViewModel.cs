using System.Collections.Generic;

namespace RecipeSharingSystem.Models
{
    public class CategoriesViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public string SelectedCategory { get; set; }
        public string ErrorMessage { get; set; }
    }
}