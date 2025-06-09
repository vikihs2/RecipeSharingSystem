using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Models;
using System.Linq;
using RecipeSharingSystem.Data;
using System;

namespace RecipeSharingSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category)
        {
            var validCategories = new[]
            {
                "Desserts",
                "Main Dishes",
                "Salads",
                "Appetizers",
                "Soups & Stews",
                "Breakfast",
                "Vegetarian / Vegan"
            };

            var selectedCategory = validCategories
                .FirstOrDefault(c => c.Equals(category, StringComparison.OrdinalIgnoreCase))
                ?? "Desserts";

            var recipes = _context.Recipes.ToList();

            var model = new CategoriesViewModel
            {
                Recipes = recipes,
                SelectedCategory = selectedCategory
            };

            return View(model);
        }
    }
}