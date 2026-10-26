using Coman_Ionut_Alexandru_Lab2.Models;

namespace Coman_Ionut_Alexandru_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
            = new List<Publisher>();

        public IEnumerable<Book>? Books { get; set; }
    }
}