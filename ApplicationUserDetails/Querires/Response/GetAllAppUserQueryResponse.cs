namespace ApplicationUserDetails.Querires.Response
{
    public class GetAllAppUserQueryResponse
    {
        public int Id { get; set; }
        public string Badge { get; set; }
        public string FullName { get; set; }
       public GetAppUserProjectResponse Project { get; set; }
        public string Password { get; set; } 
      
    }
}
