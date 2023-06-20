using AISApp.Models;

namespace AISApp.Interfaces
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<Hospital> GetByIdAsync(int id);
        Task<Hospital> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Hospital>> GetHospitalByCity(string city);

        //crud
        bool Add(Hospital hospital);
        bool Update(Hospital hospital); 
        bool Delete(Hospital hospital); 
        bool Save();

    }
}
