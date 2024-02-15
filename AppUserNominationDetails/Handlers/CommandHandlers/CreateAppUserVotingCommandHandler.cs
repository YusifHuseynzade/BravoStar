using AppUserNominationDetails.Commands.Request;
using AppUserNominationDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System;
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
            var hasVotedBefore = await _appUserNominationRepository.UserHasVotedForSameNomineeAsync(request.AppUserId, request.NominationId);
            if (hasVotedBefore)
            {
                // Kullanıcı daha önce aynı ödülü aynı kişiye vermişse başarısız yanıt dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Bu kullanıcı daha önce aynı kişiye aynı ödülü vermiş." };
            }

            // Kullanıcının kendisi haricindeki diğer kullanıcıları al
            var otherUsers = await _appUserRepository.GetUsersInSameProjectExceptCurrentUserAsync(request.CurrentUserId);

            // Verilen kullanıcının listede olup olmadığını kontrol et
            if (!otherUsers.Any(u => u.Id == request.AppUserId))
            {
                // Belirtilen kullanıcı listede yoksa başarısız yanıt dön
                return new CreateAppUserVotingCommandResponse { IsSuccess = false, ErrorMessage = "Geçersiz kullanıcı." };
            }

            // AppUser ve Nomination nesnelerini veritabanından al
            var appUser = await _appUserRepository.GetAsync(u => u.Id == request.AppUserId);
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
