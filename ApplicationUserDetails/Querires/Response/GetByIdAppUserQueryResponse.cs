namespace ApplicationUserDetails.Querires.Response
{
    public class GetByIdAppUserQueryResponse
    {
        public int Id { get; set; }
        public GetNominationResponse Nomination { get; set; }
        public GetVotedAppUserResponse VotedAppUser { get; set; }
       public GetAppUserNominationResponse AppUserNomination { get; set; }
    }
}