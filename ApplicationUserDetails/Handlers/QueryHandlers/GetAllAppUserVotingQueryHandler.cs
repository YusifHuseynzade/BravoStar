using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationUserDetails.Queries.Request;
using ApplicationUserDetails.Queries.Response;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.Handlers.QueryHandlers
{
    public class GetAllppUserVotingQueryHandler : IRequestHandler<GetAllAppUserVotingQueryRequest, List<GetAllAppUserVotingQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserNominationRepository _appUserNominationRepository;

        public GetAllppUserVotingQueryHandler(IMapper mapper, IAppUserRepository appUserRepository, IAppUserNominationRepository appUserNominationRepository)
        {
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _appUserNominationRepository = appUserNominationRepository;
        }

        public async Task<List<GetAllAppUserVotingQueryResponse>> Handle(GetAllAppUserVotingQueryRequest request, CancellationToken cancellationToken)
        {

            // Tüm oy veren kullanıcıları getir
            var appUserNominations = await _appUserNominationRepository.GetAllAsync(
                an => true,
                "AppUser", // AppUser ilişkisel verisini Include et
                "Nomination" // Nomination ilişkisel verisini Include et
            );

            // Oy veren ve oy alan kullanıcıları içeren cevap nesnelerini oluştur
            var responses = new List<GetAllAppUserVotingQueryResponse>();
            foreach (var appUserNomination in appUserNominations)
            {
                // Oy alan kullanıcıyı bul
                var selectedUser = await _appUserRepository.GetUserByIdAsync(appUserNomination.NomineeId);
                var nomineeBadge = selectedUser?.Badge ?? "Bilinmeyen Kullanıcı";

                var userCountInSelectedUserProject = await _appUserRepository.GetProjectUserCountAsync(selectedUser.ProjectId);
                var projectsUserCounts = await _appUserRepository.GetProjectsUserCountsAsync();
                var maxUserCount = projectsUserCounts.Values.Max();

                var coefficient = maxUserCount > 0 ? (float)maxUserCount / userCountInSelectedUserProject : 0;

                var response = new GetAllAppUserVotingQueryResponse
                {
                    Id = appUserNomination.Id,
                    VoiceSenderBadge = appUserNomination.AppUser?.Badge, // Oy veren kullanıcının rozeti
                    NomineeBadge = nomineeBadge, // Oy alan kullanıcının rozeti
                    Nomination = _mapper.Map<GetNominationResponse>(appUserNomination.Nomination), // Verilen nomination
                    AppUserRates = new GetAppUserRateResponse // Oy veren kullanıcının oy oranı bilgileri
                    {
                        Id = appUserNomination.AppUserId, // Oy veren kullanıcının ID'si
                        Result = appUserNomination.Rate,
                        Coefficient = coefficient,
                        Rate = appUserNomination.Rate / coefficient
                    }
                };
                responses.Add(response);
            }

            return responses;
        }
    }
}
