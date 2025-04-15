using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController: Controller
    {
        private readonly MyAppContext _context; //MyAppContext is a class provided by Entity Framework Core that represents a session with the database, allowing us to query and save data. -context is an instance of this class that is injected into the ItemsController class.
        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Items.ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price")] Item item) //The Bind attribute specifies which properties of the Item class are included in the model binding process. This helps prevent overposting attacks by only binding the specified properties.
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item); //The Add method of the DbSet class is used to add a new entity to the context.
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(item); //If the model state is not valid, the Create view is rendered again with the entered data.
        }

        public async Task<IActionResult> Edit(int id) //The Edit method takes an id parameter that specifies the id of the item to be edited.
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id); //The FirstOrDefaultAsync method is used to retrieve the item with the specified id from the database.
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")] //The ActionName attribute specifies that this method should be called when the Delete action is invoked.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id); //The FindAsync method is used to retrieve the item with the specified id from the database.
            if (item != null)
            {
                _context.Items.Remove(item); //The Remove method of the DbSet class is used to remove an entity from the context.
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
