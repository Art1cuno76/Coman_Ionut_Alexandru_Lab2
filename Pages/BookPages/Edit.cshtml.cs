
using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public EditModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Book is null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);

        PopulateDropDownLists(Book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        int? id,
        string[] selectedCategories)
    {
        if (id is null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (bookToUpdate is null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.AuthorID,
            b => b.Price,
            b => b.PublishingDate,
            b => b.PublisherID))
        {
            UpdateBookCategories(
                _context,
                selectedCategories,
                bookToUpdate
            );

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        UpdateBookCategories(
            _context,
            selectedCategories,
            bookToUpdate
        );

        PopulateAssignedCategoryData(
            _context,
            bookToUpdate
        );

        Book = bookToUpdate;

        PopulateDropDownLists(Book);

        return Page();
    }

    private void PopulateDropDownLists(Book book)
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(),
            "ID",
            "PublisherName",
            book.PublisherID
        );

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
            book.AuthorID
        );
    }
}
