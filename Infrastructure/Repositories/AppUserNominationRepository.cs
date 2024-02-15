using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppUserNominationRepository : Repository<AppUserNomination>, IAppUserNominationRepository
    {
        private readonly AppDbContext _context;

        public AppUserNominationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<AppUserNomination>> GetAllAsync(Expression<Func<AppUserNomination, bool>> predicate)
        {
            return await _context.Set<AppUserNomination>().Where(predicate).ToListAsync();
        }
        public async Task DeleteAsync(AppUserNomination entity)
        {
            _context.Set<AppUserNomination>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserHasVotedForSameNomineeAsync(int voiceSenderUserId, int selectedUserId, int nominationId)
        {
            return await _context.AppUserNominations
                .AnyAsync(n => n.AppUserId == voiceSenderUserId && n.NomineeId == selectedUserId && n.NominationId == nominationId);
        }

        public async Task<bool> UserHasVotedForNominationInProjectAsync(int userId, int nominationId)
        {
            // Kullanıcının aynı nominasyonu kendi projesinde başka bir kişiye verip vermediğini kontrol et
            return await _context.AppUserNominations
                .Include(an => an.AppUser)
                .Where(an => an.AppUser.Id == userId && an.NominationId == nominationId)
                .AnyAsync();
        }

    }
}
