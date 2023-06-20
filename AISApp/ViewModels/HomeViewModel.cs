using AISApp.Models;

namespace AISApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Hospital>? Hospitals { get; set; }
        public string? City { get; set; }
        
        
    }
}
