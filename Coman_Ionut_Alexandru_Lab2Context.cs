
using Coman_Ionut_Alexandru_Lab2.Models;
using Microsoft.EntityFrameworkCore;

public class Coman_Ionut_Alexandru_Lab2Context(
    DbContextOptions<Coman_Ionut_Alexandru_Lab2Context> options
) : DbContext(options)
{
    public DbSet<Book> Book { get; set; } = default!;

    public DbSet<Publisher> Publisher { get; set; } = default!;

    public DbSet<Author> Authors { get; set; } = default!;
}
