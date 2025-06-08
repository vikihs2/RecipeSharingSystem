using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Data;

namespace RecipeSharingSystem.Web.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrivacyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}