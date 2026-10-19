using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coman_Ionut_Alexandru_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public CreateModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = new Book
    {
        BookCategories = new List<BookCategory>()
    };

    public IActionResult OnGet()
    {
        PopulateDropDownLists();
        PopulateAssignedCategoryData(_context, Book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        Book.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var categoryID in selectedCategories)
            {
                Book.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(categoryID)
                });
            }
        }

        if (!ModelState.IsValid)
        {
            PopulateDropDownLists();
            PopulateAssignedCategoryData(_context, Book);

            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void PopulateDropDownLists()
    {
        var authorList = _context.Authors
            .Select(a => new
            {
                a.ID,
                FullName = a.LastName + " " + a.FirstName
            })
            .ToList();

        ViewData["AuthorID"] = new SelectList(
            authorList,
            "ID",
            "FullName",
            Book.AuthorID
        );

        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            Book.PublisherID
        );
    }
}