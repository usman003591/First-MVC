using Microsoft.AspNetCore.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Overview() //IActionResult is a return type that represents an HTTP response in an ASP.NET Core MVC application. It can return a view, a file, a redirect, etc.
        {
            var item = new Item() { Name = "My Item" };
            return View(item);
        }

        public IActionResult Edit(int itemId)
        {
            return Content("id = " + itemId);
        }
    }
}
