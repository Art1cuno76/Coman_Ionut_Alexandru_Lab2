
using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public CreateModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Set<Publisher>(),
            "ID",
            "PublisherName"
        );

        ViewData["AuthorID"] = new SelectList(
            _context.Authors,
            "ID",
            "LastName"
        );

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
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

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
