using Microsoft.AspNetCore.Mvc;
using RecipeSharingSystem.Data;

namespace RecipeSharingSystem.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}