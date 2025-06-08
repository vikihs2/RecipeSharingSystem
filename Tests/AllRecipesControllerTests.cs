using Xunit;
using RecipeSharingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RecipeSharingSystem.Tests
{
    public class RecipeModelTests
    {
        [Fact]
        public void RecipeModel_WithoutTitle_ShouldBeInvalid()
        {
            var model = new Recipe();
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("Title"));
        }
    }
}
// testva - reqired missing fields
//dotnet test