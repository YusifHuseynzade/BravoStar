namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public List<AppUserRole>? AppUserRoles { get; set; }
		public void SetDetail(string name)
		{
			this.RoleName = name;
		}
	}
}
