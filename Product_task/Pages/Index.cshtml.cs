using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Products.Models;
using Products.Settings;

namespace Product_task.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Items NewItem { get; set; }

        [BindProperty(SupportsGet = true)]
        public string All { get; set; }

        [BindProperty]
        public string NameCategory { get; set; }

        public Items Item { get; set; }
        public Category Category { get; set; }
        public List<Items> AllItems { get; set; }

        public IActionResult OnGet(string name)
        {
            var categories = _context.Categories.Include(c => c.Items)
                .Where(c => c.Name == name)
                .ToList();

            if (categories.Count > 0)
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




        public async Task<IActionResult> OnPost(string nameItem, string nameCategory)
        {
            if (string.IsNullOrEmpty(nameItem) || string.IsNullOrEmpty(nameCategory))
            {
                return Content("Error");
            }

            var newItem = new Items
            {
                Name = nameItem
            };

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == nameCategory);

            if (category == null)
            {
                var newCategory = new Category
                {
                    Name = nameCategory
                };

                await _context.Categories.AddAsync(newCategory); 
                await _context.SaveChangesAsync(); 

                newItem.CategoryId = newCategory.Id; 
            }
            else
            {
                newItem.CategoryId = category.Id; 
            }

            await _context.Products.AddAsync(newItem); 
            await _context.SaveChangesAsync(); 

            return Content("Add success");
        }

        


    }
}
