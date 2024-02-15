using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace ApplicationUserDetails.Handlers.CommandHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommandRequest, UpdateAppUserCommandResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAppUserNominationRepository _appUserNominationRepository;
        private readonly INominationRepository _nominationRepository;


        public UpdateAppUserCommandHandler(IAppUserRepository userRepository, IAppUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _appUserNominationRepository = appUserNominationRepository;
            _nominationRepository = nominationRepository;
        }

        public async Task<UpdateAppUserCommandResponse> Handle(UpdateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateAppUserCommandResponse();

            try
            {
                // Retrieve existing user
                var existingUser = await _userRepository.GetAsync(u => u.Id == request.Id);

                existingUser.SetDetails(request.FullName, request.Badge, request.Password);



                existingUser.SetProject(request.ProjectId);


                // Update user
                await _userRepository.UpdateAsync(existingUser);
                await _userRepository.CommitAsync();

                // Assign roles if provided
                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    //// Remove existing roles
                    var existingRoles = await _userRoleRepository.GetAllAsync(ur => ur.AppUserId == existingUser.Id);
                    foreach (var role in existingRoles)
                    {
                        await _userRoleRepository.DeleteAsync(role);
                    }

                    // Add new roles
                    foreach (var roleId in request.RoleIds)
                    {
                        var role = await _roleRepository.GetAsync(r => r.Id == roleId);
                        if (role != null)
                        {
                            var appUserRole = new AppUserRole
                            {
                                AppUserId = existingUser.Id,
                                RoleId = roleId
                            };
                            await _userRoleRepository.AddAsync(appUserRole);
                        }
                    }

                    await _userRoleRepository.CommitAsync();
                }


                response.IsSuccess = true;
                response.Message = "User updated successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception
                response.IsSuccess = false;
                response.Message = "An error occurred while updating the user.";
                // You can log the exception here
            }

            return response;
        }
    }
}
