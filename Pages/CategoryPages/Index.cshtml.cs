using Coman_Ionut_Alexandru_Lab2.Models;
using Coman_Ionut_Alexandru_Lab2.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public IndexModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; }
        = new List<Category>();

    public CategoryIndexData CategoryData { get; set; }
        = new CategoryIndexData();

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();

        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .AsNoTracking()
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        Category = CategoryData.Categories.ToList();

        if (id.HasValue)
        {
            var selectedCategory = CategoryData.Categories
                .FirstOrDefault(c => c.ID == id.Value);

            if (selectedCategory != null)
            {
                CategoryID = selectedCategory.ID;

                CategoryData.Books = selectedCategory.BookCategories?
                    .Select(bc => bc.Book)
                    .DistinctBy(b => b.Id)
                    .OrderBy(b => b.Title)
                    .ToList() ?? new List<Book>();
            }
        }
    }
}