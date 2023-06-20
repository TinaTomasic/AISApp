using AISApp.Data;
using AISApp.Interfaces;
using AISApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AISApp.Repository
{
    //unosim db tabele sa ovim dole
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationDbContext _context;

        public HospitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Hospital hospital)
        {
            _context.Add(hospital);
            //kreiram sql
            return Save();
            //save vrednost u bazu
        }

        public bool Delete(Hospital hospital)
        {
            _context.Remove(hospital);
            return Save();
        }

        //task je kao buzzer za popularan restoran dok cekam sto
        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Hospital> GetByIdAsync(int id)
        {
            return await _context.Hospitals.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Hospital> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Hospitals.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Hospital>> GetHospitalByCity(string city)
        {
            return await _context.Hospitals.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Hospital hospital)
        {
            _context.Update(hospital);
            return Save();
        }
    }
}
