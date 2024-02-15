using AppUserNominationDetails.Commands.Request;
using AppUserNominationDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace AppUserNominationDetails.Handlers.CommandHandlers;

public class CreateAppUserVotingCommandHandler : IRequestHandler<CreateAppUserVotingCommandRequest, CreateAppUserVotingCommandResponse>
{
    private readonly IAppUserRepository _repository;
    private readonly IAppUserNominationRepository _appUserNominationRepository;
    private readonly INominationRepository _nominationRepository;


    public CreateAppUserVotingCommandHandler(IAppUserRepository repository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
    {
        _repository = repository;
        _appUserNominationRepository = appUserNominationRepository;
        _nominationRepository = nominationRepository;
    }
    public async Task<CreateAppUserVotingCommandResponse> Handle(CreateAppUserVotingCommandRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcının projeye ait olup olmadığını doğrula
        var appUser = await _repository.GetAsync(u => u.Id == request.AppUserId, nameof(AppUser.Project));
        if (appUser == null)
        {
            // Kullanıcı bulunamadı, işlem başarısız
            return new CreateAppUserVotingCommandResponse { IsSuccess = false };
        }

        // Kullanıcının ait olduğu proje
        var project = appUser.Project;

        // Oylamanın yapılacağı kişilerin listesi
        var appUsersToVote = project.AppUsers.Where(u => u.Id != appUser.Id).ToList();

        // Verilen oy sayısı ve toplam puanı takip etmek için değişkenler
        int totalVotes = 0;
        int totalPoints = 0;

        // Kullanıcının her bir kişiye oy vermesi
        foreach (var appUserToVote in appUsersToVote)
        {
            // Verilen oy ve puan
            var nominationRequest = request.AppUserNominations.FirstOrDefault(n => n.SameProjectUserId == appUserToVote.Id);
            if (nominationRequest != null)
            {
                // Eğer kullanıcı bu kişiye oy vermişse
                if (nominationRequest.Nomination != null && nominationRequest.Nomination.Rate >= 1 && nominationRequest.Nomination.Rate <= 5)
                {
                    // Yeni bir oy oluştur
                    var newNomination = new AppUserNomination
                    {
                        AppUserId = appUserToVote.Id,
                        Rate = nominationRequest.Nomination.Rate,
                        CreatedAt = DateTime.UtcNow.AddHours(4) // UTC+4 olarak zamanı ayarla
                    };

                    // Veritabanına ekle
                    await _appUserNominationRepository.AddAsync(newNomination);

                    // Toplam oy sayısını ve puanı güncelle
                    totalVotes++;
                    totalPoints += (int)nominationRequest.Nomination.Rate;
                }
            }
        }

        // Ortalama puanı hesapla
        float averageRating = totalVotes > 0 ? (float)totalPoints / totalVotes : 0;

        // Kullanıcıyı güncelle
        await _repository.UpdateAsync(appUser);

        // İşlem başarılı
        return new CreateAppUserVotingCommandResponse { IsSuccess = true };
    }
}
