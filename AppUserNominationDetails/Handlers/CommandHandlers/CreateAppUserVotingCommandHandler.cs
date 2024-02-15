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

        public CreateAppUserVotingCommandHandler(IAppUserRepository appUserRepository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
        {
            _appUserRepository = appUserRepository;
            _appUserNominationRepository = appUserNominationRepository;
            _nominationRepository = nominationRepository;
        }

        public async Task<CreateAppUserVotingCommandResponse> Handle(CreateAppUserVotingCommandRequest request, CancellationToken cancellationToken)
        {
            var hasVotedBefore = await _appUserNominationRepository.UserHasVotedForSameNomineeAsync(request.VoiceSenderUserId, request.SelectedUserId, request.NominationId);
            if (hasVotedBefore)
            {
                // Kullanıcı daha önce aynı ödülü aynı kişiye vermişse başarısız yanıt dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Bu kullanıcı daha önce aynı kişiye aynı ödülü vermiş." };
            }

            // Belirtilen kullanıcının kendi projesinde aynı nominasyonu başka bir kişiye verdiğini kontrol et
            var hasVotedForNominationInProject = await _appUserNominationRepository.UserHasVotedForNominationInProjectAsync(request.VoiceSenderUserId, request.NominationId);
            if (hasVotedForNominationInProject)
            {
                // Kullanıcı daha önce kendi projesinde aynı ödülü başka bir kişiye vermişse başarısız yanıt dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Bu kullanıcı daha önce aynı ödülü kendi projesinde başka bir kişiye vermiş." };
            }

            // Kullanıcının kendisi haricindeki diğer kullanıcıları al
            var otherUsers = await _appUserRepository.GetUsersInSameProjectExceptCurrentUserAsync(request.VoiceSenderUserId);

            // Verilen kullanıcının listede olup olmadığını kontrol et
            if (!otherUsers.Any(u => u.Id == request.SelectedUserId))
            {
                // Belirtilen kullanıcı listede yoksa başarısız yanıt dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Geçersiz kullanıcı." };
            }

            // AppUser ve Nomination nesnelerini veritabanından al
            var appUser = await _appUserRepository.GetAsync(u => u.Id == request.VoiceSenderUserId);
            var nomination = await _nominationRepository.GetAsync(n => n.Id == request.NominationId);

            if (appUser == null || nomination == null)
            {
                // AppUser veya Nomination bulunamazsa başarısız yanıtı dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "AppUser veya Nomination bulunamadı." };
            }

            // Yeni AppUserNomination nesnesi oluştur
            var appUserNomination = new AppUserNomination
            {
                AppUser = appUser,
                Nomination = nomination,
                Rate = request.Rate,
                NomineeId = request.SelectedUserId,
                CreatedAt = DateTime.UtcNow // Oy verildiği zamanı kaydet
            };

            // Yeni AppUserNomination nesnesini veritabanına ekle
            await _appUserNominationRepository.AddAsync(appUserNomination);
            await _appUserNominationRepository.CommitAsync();

            // Başarılı yanıtı dön
            return new CreateAppUserVotingCommandResponse { IsSuccess = true };
        }
    }
}
