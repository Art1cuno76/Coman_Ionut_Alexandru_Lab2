using Coman_Ionut_Alexandru_Lab2.Models;
using Coman_Ionut_Alexandru_Lab2.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Coman_Ionut_Alexandru_Lab2.Pages.PublisherPages;

public class IndexModel : PageModel
{
    private readonly Coman_Ionut_Alexandru_Lab2Context _context;

    public IndexModel(Coman_Ionut_Alexandru_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; }
        = new List<Publisher>();

    public PublisherIndexData PublisherData { get; set; }
        = new PublisherIndexData();

    public int PublisherID { get; set; }

    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();

        PublisherData.Publishers = await _context.Publisher
            .Include(p => p.Books)
                .ThenInclude(b => b.Author)
            .AsNoTracking()
            .OrderBy(p => p.PublisherName)
            .ToListAsync();

        Publisher = PublisherData.Publishers.ToList();

        if (id.HasValue)
        {
            var selectedPublisher = PublisherData.Publishers
                .FirstOrDefault(p => p.ID == id.Value);

            if (selectedPublisher != null)
            {
                PublisherID = selectedPublisher.ID;

                PublisherData.Books = selectedPublisher.Books
                    ?? new List<Book>();
            }
        }
    }
}