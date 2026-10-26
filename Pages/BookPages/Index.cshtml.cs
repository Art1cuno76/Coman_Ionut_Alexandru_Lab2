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

    public string TitleSort { get; set; } = string.Empty;

    public string AuthorSort { get; set; } = string.Empty;

    public string CurrentFilter { get; set; } = string.Empty;

    public async Task OnGetAsync(
        int? id,
        int? categoryID,
        string? sortOrder,
        string? searchString)
    {
        TitleSort = string.IsNullOrEmpty(sortOrder)
            ? "title_desc"
            : "";

        AuthorSort = sortOrder == "author"
            ? "author_desc"
            : "author";

        CurrentFilter = searchString?.Trim() ?? string.Empty;

        BookD = new BookData();

        BookD.Books = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .ToListAsync();

        if (!string.IsNullOrEmpty(CurrentFilter))
        {
            BookD.Books = BookD.Books.Where(b =>
                b.Title.Contains(
                    CurrentFilter,
                    StringComparison.OrdinalIgnoreCase)
                ||
                (b.Author?.FullName.Contains(
                    CurrentFilter,
                    StringComparison.OrdinalIgnoreCase) ?? false));
        }

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

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Title);
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Author?.FullName);
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Author?.FullName);
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Title);
                break;
        }

        Book = BookD.Books.ToList();
    }
}