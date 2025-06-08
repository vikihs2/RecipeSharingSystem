using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace RecipeSharingSystem.Tests
{
    public class RecipeFilterTests
    {
        [Fact]
        public void Filter_By_Title_Should_Return_Matching_Recipes()
        {
            var recipes = new List<string> { "Pizza", "Pasta", "Soup" };
            var result = recipes.Where(r => r.Contains("P")).ToList();

            Assert.Equal(3, result.Count);
        }
    }
}
//testva dali imam filtering recipes
//dotnet test