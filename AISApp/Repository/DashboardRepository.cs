﻿using AISApp.Data;
using AISApp.Interfaces;
using AISApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AISApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Hospital>> GetAllUserHospitals()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userHospitals = _context.Hospitals.Where(r => r.AppUser.Id == curUser);
            return userHospitals.ToList();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
