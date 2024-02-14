namespace ApplicationUserDetails.Commands.Response
{
    public class LoginAppUserCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}