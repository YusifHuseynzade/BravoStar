using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Common.Interfaces;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationUserDetails.Handlers.QueryHandlers
{
    public class GetByIdAppUserQueryHandler : IRequestHandler<GetByIdAppUserQueryRequest, List<GetByAppUserIdVotedAppUserListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetByIdAppUserQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetByAppUserIdVotedAppUserListResponse>> Handle(GetByIdAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            request.NormalizeDates();
            var userId = request.Id;

            // Kullanıcıyı veritabanından al
            var user = await _context.AppUsers
                .Include(u => u.AppUserNominations)
                    .ThenInclude(an => an.Nomination)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
            {
                // Kullanıcı bulunamadı, null döndür
                return null;
            }

            // Kullanıcının oy kullandığı diğer kullanıcıları al
            var votedUsers = user.AppUserNominations
                .Where(an => an.NomineeId != userId)
                .Where(an => (request.StartDate == null || an.CreatedAt >= request.StartDate) &&
                             (request.EndDate == null || an.CreatedAt <= request.EndDate)) 
                .Select(an => new GetByIdAppUserQueryResponse
                {
                    Id = an.NomineeId,
                    Nomination = _mapper.Map<GetNominationResponse>(an.Nomination),
                    VotedAppUser = _mapper.Map<GetVotedAppUserResponse>(an.AppUser),
                    AppUserNomination = _mapper.Map<GetAppUserNominationResponse>(an)
                })
                .ToList();


            if (request.ShowMore != null)
            {
                votedUsers = votedUsers.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take)
                                   .ToList();
            }

            var totalCount = user.AppUserNominations.Count;

            PaginationListDto<GetByIdAppUserQueryResponse> model =
              new PaginationListDto<GetByIdAppUserQueryResponse>(votedUsers, request.Page, request.ShowMore?.Take ?? votedUsers.Count, totalCount);

            return new List<GetByAppUserIdVotedAppUserListResponse>
            {
                new GetByAppUserIdVotedAppUserListResponse
                {
                    TotalVotedAppUserCount = totalCount,
                    AppUsers = model.Items
                }
            };
        }
    }
}
