using AISApp.Models;

namespace AISApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Hospital>> GetAllUserHospitals();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
