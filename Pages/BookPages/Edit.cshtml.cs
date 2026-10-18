
using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.BookPages;

public class EditModel : PageModel
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

        var book = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;

        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(),
            "ID",
            "PublisherName",
            Book.PublisherID
        );

        ViewData["AuthorID"] = new SelectList(
            _context.Authors,
            "ID",
            "LastName",
            Book.AuthorID
        );

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(
                _context.Set<Publisher>(),
                "ID",
                "PublisherName",
                Book.PublisherID
            );

            ViewData["AuthorID"] = new SelectList(
                _context.Authors,
                "ID",
                "LastName",
                Book.AuthorID
            );

            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.Id == id);
    }
}
