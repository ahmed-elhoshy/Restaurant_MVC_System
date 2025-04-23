using Microsoft.AspNetCore.Mvc;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;
        public IngredientController (ApplicationDbContext db)
        {
           ingredients= new Repository<Ingredient> (db);
        }
        public async Task <IActionResult> Index()
        {
            return View(await ingredients.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ingredient = await ingredients.GetByIdAsync(id, new QueryOptions<Ingredient>
            {
                Includes = "ProductIngredients.Product"
            });

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

    }
}
