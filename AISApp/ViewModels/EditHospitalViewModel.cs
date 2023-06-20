using AISApp.Data.Enum;
using AISApp.Models;

namespace AISApp.ViewModels
{
    public class EditHospitalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string? URL { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public HospitalCategory HospitalCategory { get; set; }
    }
}
