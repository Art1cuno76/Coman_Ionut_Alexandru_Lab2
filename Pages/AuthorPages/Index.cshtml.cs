using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Coman_Ionut_Alexandru_Lab2.Models;

namespace Coman_Ionut_Alexandru_Lab2.Pages.AuthorPages;

public class IndexModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public IndexModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IList<Author> Author { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Author = await _context.Authors.ToListAsync();
    }
}
