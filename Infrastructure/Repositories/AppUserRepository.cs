using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            return await _context.AppUsers.FindAsync(userId);
        }

        public async Task<List<AppUser>> GetUsersInSameProjectExceptCurrentUserAsync(int currentUserId)
        {
            // Şu anki kullanıcının ait olduğu proje ID'sini al
            var currentUser = await GetUserByIdAsync(currentUserId);
            var projectId = currentUser.ProjectId;

            // Aynı projede bulunan diğer kullanıcıları al, ancak şu anki kullanıcıyı hariç tut
            var otherUsers = await _context.AppUsers.Where(u => u.ProjectId == projectId && u.Id != currentUserId).ToListAsync();

            return otherUsers;
        }

        public async Task<int> GetUserProjectIdAsync(int userId)
        {
            var user = await _context.AppUsers.FindAsync(userId);
            return user?.ProjectId ?? 0;
        }

        public async Task<Dictionary<int, int>> GetProjectsUserCountsAsync()
        {
            var users = await _context.AppUsers.ToListAsync();
            var projectUserCounts = users
                .GroupBy(u => u.ProjectId)
                .ToDictionary(g => g.Key, g => g.Count());

            return projectUserCounts;
        }

        public async Task<int> GetProjectUserCountAsync(int projectId)
        {
            var projectUserCount = await _context.AppUsers.CountAsync(u => u.ProjectId == projectId);
            return projectUserCount;
        }

       

    }
}
