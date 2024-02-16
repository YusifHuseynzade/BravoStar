using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetUserByIdAsync(int userId);
        Task<List<AppUser>> GetUsersInSameProjectExceptCurrentUserAsync(int currentUserId);
        Task<Dictionary<int, int>> GetProjectsUserCountsAsync();
        Task<int> GetUserProjectIdAsync(int userId);

        Task<int> GetProjectUserCountAsync(int projectId);
       

    }
}
