using ApplicationUserDetails.Querires.Response;

namespace ApplicationUserDetails.Queries.Response;

public class GetAllAppUserVotingQueryResponse
{
    public int Id { get; set; }
    public string VoiceSenderBadge { get; set; }
    public string NomineeBadge { get; set; }
    public GetNominationResponse Nomination { get; set; }
    public GetAppUserRateResponse AppUserRates { get; set; }
}
