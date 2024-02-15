using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IAppUserNominationRepository : IRepository<AppUserNomination>
    {
        Task<List<AppUserNomination>> GetAllAsync(Expression<Func<AppUserNomination, bool>> predicate);
        Task DeleteAsync(AppUserNomination entity);
        Task<bool> UserHasVotedForSameNomineeAsync(int voiceSenderUserId, int selectedUserId, int nominationId);
        Task<bool> UserHasVotedForNominationInProjectAsync(int userId, int nominationId);

    }
}
