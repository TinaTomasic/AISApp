using AISApp.Models;
using AISApp.Data.Enum;

namespace AISApp.ViewModels
{
    public class CreateHospitalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public string Image { get; set; }
        public HospitalCategory HospitalCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
