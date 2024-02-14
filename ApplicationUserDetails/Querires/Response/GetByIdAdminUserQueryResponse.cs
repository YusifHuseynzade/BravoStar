namespace ApplicationUserDetails.Querires.Response
{
    public class GetByIdAdminUserQueryResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}