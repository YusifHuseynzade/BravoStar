using AppUserNominationDetails.Commands.Request;
using AppUserNominationDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppUserNominationDetails.Handlers.CommandHandlers
{
    public class CreateAppUserVotingCommandHandler : IRequestHandler<CreateAppUserVotingCommandRequest, CreateAppUserVotingCommandResponse>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserNominationRepository _appUserNominationRepository;
        private readonly INominationRepository _nominationRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateAppUserVotingCommandHandler(IAppUserRepository appUserRepository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository, IProjectRepository projectRepository)
        {
            _appUserRepository = appUserRepository;
            _appUserNominationRepository = appUserNominationRepository;
            _nominationRepository = nominationRepository;
            _projectRepository = projectRepository;
        }

        public async Task<CreateAppUserVotingCommandResponse> Handle(CreateAppUserVotingCommandRequest request, CancellationToken cancellationToken)
        {
            foreach (var selectedUser in request.SelectedUsers)
            {
                var hasVotedBefore = await _appUserNominationRepository.UserHasVotedForSameNomineeAsync(request.VoiceSenderUserId, selectedUser.SelectedUserId, selectedUser.NominationId);
                if (hasVotedBefore)
                {
                    return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Bu kullanıcı daha önce aynı kişiye aynı ödülü vermiş." };
                }

                var hasVotedForNominationInProject = await _appUserNominationRepository.UserHasVotedForNominationInProjectAsync(request.VoiceSenderUserId, selectedUser.NominationId);
                if (hasVotedForNominationInProject)
                {
                    return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Bu kullanıcı daha önce aynı ödülü kendi projesinde başka bir kişiye vermiş." };
                }

                var otherUsers = await _appUserRepository.GetUsersInSameProjectExceptCurrentUserAsync(request.VoiceSenderUserId);

                if (!otherUsers.Any(u => u.Id == selectedUser.SelectedUserId))
                {
                    return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Geçersiz kullanıcı." };
                }

                var appUser = await _appUserRepository.GetAsync(u => u.Id == request.VoiceSenderUserId);
                var nomination = await _nominationRepository.GetAsync(n => n.Id == selectedUser.NominationId);

                if (appUser == null || nomination == null)
                {
                    return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "AppUser veya Nomination bulunamadı." };
                }

                var projectsUserCounts = await _appUserRepository.GetProjectsUserCountsAsync();
                var senderUserProjectId = await _appUserRepository.GetUserProjectIdAsync(request.VoiceSenderUserId);
                var senderUserProjectUserCount = projectsUserCounts.GetValueOrDefault(senderUserProjectId, 0);

                if (senderUserProjectUserCount == 0)
                {
                    return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Kullanıcı bulunamadı." };
                }

                var maxUserCount = projectsUserCounts.Values.Max();
                var ratio = (float)maxUserCount / senderUserProjectUserCount;
                var calculatedRate = selectedUser.Rate * ratio;

                var appUserNomination = new AppUserNomination
                {
                    AppUser = appUser,
                    Nomination = nomination,
                    Rate = calculatedRate,
                    NomineeId = selectedUser.SelectedUserId,
                    CreatedAt = DateTime.UtcNow // Oy verildiği zamanı kaydet
                };

                await _appUserNominationRepository.AddAsync(appUserNomination);
                await _appUserNominationRepository.CommitAsync();
            }

            return new CreateAppUserVotingCommandResponse { IsSuccess = true };
        }
    }
}
