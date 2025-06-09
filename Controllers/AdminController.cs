using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Models;
using RecipeSharingSystem.Data;
using System.Linq;

namespace RecipeSharingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminPanel()
        {
            var model = new AdminPanelViewModel
            {
                PendingRecipes = _context.Recipes
                    .Where(r => !r.IsApproved)
                    .ToList(),
                AllRecipes = _context.Recipes
                    .Where(r => r.IsApproved)
                    .ToList(),
                FeaturedRecipeIds = _context.Recipes
                    .Where(r => r.IsFeatured)
                    .Select(r => r.Id)
                    .ToList(),
                TotalUsers = _context.Recipes
                    .Select(r => r.AuthorUsername)
                    .Distinct()
                    .Count(),
                TotalRecipes = _context.Recipes.Count(),
                TopContributor = _context.Recipes
                    .GroupBy(r => r.AuthorUsername)
                    .Select(g => new TopContributorViewModel
                    {
                        UserName = g.Key,
                        RecipeCount = g.Count()
                    })
                    .OrderByDescending(tc => tc.RecipeCount)
                    .FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                recipe.IsApproved = true;
                _context.SaveChanges();
            }
            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public IActionResult SetFeatured(List<int> featuredIds)
        {
            var allRecipes = _context.Recipes.Where(r => r.IsFeatured).ToList();
            foreach (var recipe in allRecipes)
            {
                recipe.IsFeatured = false;
            }

            if (featuredIds != null && featuredIds.Any())
            {
                var toFeature = _context.Recipes
                    .Where(r => featuredIds.Contains(r.Id))
                    .Take(4)
                    .ToList();
                foreach (var recipe in toFeature)
                {
                    recipe.IsFeatured = true;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("AdminPanel");
        }

        public IActionResult Edit(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe == null)
                return NotFound();
            return View(recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe updatedRecipe)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == updatedRecipe.Id);
            if (recipe == null)
                return NotFound();

            recipe.Title = updatedRecipe.Title;
            recipe.Description = updatedRecipe.Description;
            recipe.Category = updatedRecipe.Category;
            recipe.TimeToMake = updatedRecipe.TimeToMake;
            recipe.Servings = updatedRecipe.Servings;
            recipe.PhotoUrl = updatedRecipe.PhotoUrl;

            _context.SaveChanges();
            return RedirectToAction("AdminPanel");
        }
    }
}