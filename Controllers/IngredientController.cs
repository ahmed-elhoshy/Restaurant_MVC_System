using Microsoft.AspNetCore.Mvc;
using Resturant.Data;
using Resturant.Models;

namespace Resturant.Controllers
{
    public class IngredientController : Controller
    {
        private Repository<Ingredient> ingredients;
        public IngredientController(ApplicationDbContext db)
        {
            ingredients = new Repository<Ingredient>(db);
        }
        public async Task<IActionResult> Index()
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.AddAsync(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await ingredients.DeleteAsync(ingredient.IngredientId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await ingredients.UpdateAsync(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }
    }
}
