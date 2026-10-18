using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coman_Ionut_Alexandru_Lab2.Models;

namespace Coman_Ionut_Alexandru_Lab2.Pages.PublisherPages;

public class CreateModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public CreateModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Publisher.Add(Publisher);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
