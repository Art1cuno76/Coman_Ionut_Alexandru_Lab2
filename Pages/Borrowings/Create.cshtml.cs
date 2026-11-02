using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.Borrowings;

public class CreateModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public CreateModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = new Borrowing();

    public IActionResult OnGet()
    {
        PopulateDropDownLists();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropDownLists();

            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void PopulateDropDownLists()
    {
        var books = _context.Book
            .Include(b => b.Author)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToList();

        var bookList = books.Select(b => new
        {
            b.Id,
            BookFullName = b.Title + " - " +
                (b.Author == null
                    ? "Unknown author"
                    : b.Author.LastName + " " + b.Author.FirstName)
        });

        ViewData["BookID"] = new SelectList(
            bookList,
            "Id",
            "BookFullName",
            Borrowing.BookID
        );

        var members = _context.Member
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ToList();

        ViewData["MemberID"] = new SelectList(
            members,
            "ID",
            "FullName",
            Borrowing.MemberID
        );
    }
}