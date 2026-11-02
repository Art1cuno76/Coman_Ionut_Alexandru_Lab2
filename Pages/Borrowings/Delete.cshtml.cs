using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.Borrowings;

public class DeleteModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public DeleteModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(book => book.Author)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.ID == id);

        if (borrowing is null)
        {
            return NotFound();
        }

        Borrowing = borrowing;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FindAsync(id);

        if (borrowing is null)
        {
            return NotFound();
        }

        _context.Borrowing.Remove(borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}