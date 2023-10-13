using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Models;
using Products.Settings;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(string nameItem, string nameCategory)
        {
            var newItem = new Items
            {
                Name = nameItem
            };
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == nameCategory);
            if (category == null)
            {
                var newCategory = new Category { 
                    Name = nameCategory 
                };
            }
            newItem.CategoryId = category.Id;
            await _context.Products.AddAsync(newItem);
            await _context.SaveChangesAsync();
            return View("Продукт добавлен успешно");
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string nameCategory)
        {
            var newCategory = new Category
            {
                Name = nameCategory
            };
            await  _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return View("Категория добавлена успешно");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemsFromCategory(string nameCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == nameCategory);
            if (category == null)
            { return BadRequest(); }
            var allItems = await _context.Products.Select(p => p.Id == category.Id).ToListAsync();
            return View(allItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryFromItem(string nameItem)
        {
            var item = await _context.Products.FirstOrDefaultAsync(p => p.Name == nameItem);
            if (item == null) {return BadRequest();}
            var category = await _context.Categories.FirstOrDefaultAsync(i => i.Id == item.CategoryId);
            return View(category);
        }

    }
}
