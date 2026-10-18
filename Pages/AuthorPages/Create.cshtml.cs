
using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coman_Ionut_Alexandru_Lab2.Pages.AuthorPages;

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
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Authors.Add(Author);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
