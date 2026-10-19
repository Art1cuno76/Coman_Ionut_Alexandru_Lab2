using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public IndexModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = new List<Book>();

    public BookData BookD { get; set; } = new BookData();

    public int BookID { get; set; }

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD = new BookData();

        BookD.Books = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        Book = BookD.Books.ToList();

        if (id.HasValue)
        {
            var selectedBook = BookD.Books
                .FirstOrDefault(b => b.Id == id.Value);

            if (selectedBook != null)
            {
                BookID = selectedBook.Id;

                BookD.Categories = selectedBook.BookCategories?
                    .Select(bc => bc.Category)
                    .ToList() ?? new List<Category>();
            }
        }
    }
}