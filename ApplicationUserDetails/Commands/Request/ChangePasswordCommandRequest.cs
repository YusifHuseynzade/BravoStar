using ApplicationUserDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.Commands.Request
{
    public class ChangePasswordCommandRequest : IRequest<ChangePasswordCommandResponse>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassword { get; set; }
    }
}
