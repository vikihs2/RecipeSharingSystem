using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Data;
using RecipeSharingSystem.Models;

namespace RecipeSharingSystem.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.CreatedOn = DateTime.Now;
                recipe.AuthorUsername = User.Identity?.Name;
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(recipe);
        }
    }
}