using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Data;
using System.Linq;

namespace RecipeSharingSystem.Controllers
{
    public class AllRecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllRecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var recipes = _context.Recipes.ToList();
            return View(recipes);
        }
    }
}