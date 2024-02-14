namespace Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; }
        public string Badge { get; set; }
        public string Password { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string? RefreshToken { get; set; }
        public string? OTPToken { get; set; }
        public DateTime OTPTokenCreated { get; set; }
        public DateTime OTPTokenExpires { get; set; }
        public List<AppUserNomination> AppUserNominations { get; set; }
        public List<AppUserRole>? AppUserRoles { get; set; }

        public void SetDetails(string fullName, string badge, string password)
        {
            FullName = fullName;
            Badge = badge;
            Password = password;
        }

        public void SetProject(int projectId)
        {
            ProjectId = projectId;
        }

    }
}