using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var recipes = _context.Recipes.ToList();
            ViewBag.Recipes = recipes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.CreatedOn = DateTime.Now;
                recipe.AuthorUsername = User.Identity?.Name;
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Add));
            }

            var recipes = _context.Recipes.ToList();
            ViewBag.Recipes = recipes;
            return View(recipe);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            if (recipe.AuthorUsername != User.Identity?.Name)
                return Forbid();

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            if (id != recipe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.Recipes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
                    if (existing == null || existing.AuthorUsername != User.Identity?.Name)
                        return Forbid();

                    recipe.AuthorUsername = existing.AuthorUsername;
                    recipe.CreatedOn = existing.CreatedOn;
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Recipes.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Add));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            if (recipe.AuthorUsername != User.Identity?.Name)
                return Forbid();

            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            if (recipe.AuthorUsername != User.Identity?.Name)
                return Forbid();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Add));
        }
    }
}