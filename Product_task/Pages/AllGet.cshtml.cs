using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Products.Settings;

namespace Product_task.Pages
{
    public class AllGetModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllGetModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
                var categories = _context.Categories.Include(_c => _c.Items).ToList();
                if (categories.Any())
                {
                    var result = "<table>";
                    foreach (var category in categories)
                    {
                        foreach (var item in category.Items)
                        {
                            result += $"<tr><td>{category.Name}</td><td>{item.Name}</td></tr>";
                        }
                    }
                    result += "</table>";
                    return Content(result, "text/html; charset=UTF-8");
                }
            
            return Page();
        }
    }
}
