using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Models;
using System.Linq;
using RecipeSharingSystem.Data;

namespace RecipeSharingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category, string errorMessage = null)
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

    if (!string.IsNullOrEmpty(category))
    {
        var matchedCategory = validCategories
            .FirstOrDefault(c => c.Equals(category, StringComparison.OrdinalIgnoreCase));
        if (matchedCategory != null)
        {
            return RedirectToAction("Index", "Categories", new { category = matchedCategory });
        }
        else
        {
            ViewBag.ErrorMessage = "Category not found.";
        }
    }

    var recipes = _context.Recipes
        .Where(r => r.IsApproved && r.IsFeatured)
        .ToList();

    return View(recipes);
}
        public IActionResult About()
        {
            return View();
        }
    }
}