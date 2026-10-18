namespace Coman_Ionut_Alexandru_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }
    }
}