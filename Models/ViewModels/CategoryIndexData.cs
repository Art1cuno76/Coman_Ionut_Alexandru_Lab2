using Coman_Ionut_Alexandru_Lab2.Models;

namespace Coman_Ionut_Alexandru_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; }
            = new List<Category>();

        public IEnumerable<Book>? Books { get; set; }
    }
}