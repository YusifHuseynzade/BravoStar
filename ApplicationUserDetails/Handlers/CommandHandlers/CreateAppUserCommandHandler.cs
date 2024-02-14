using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.ApplicationUserDetails.Handlers.CommandHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAppUserNominationRepository _appUserNominationRepository;
        private readonly INominationRepository _nominationRepository;



        public CreateAppUserCommandHandler(IAppUserRepository userRepository, IAppUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _appUserNominationRepository = appUserNominationRepository;
            _nominationRepository = nominationRepository;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAppUserCommandResponse();

            try
            {
                var newUser = new AppUser();
                newUser.SetDetails(request.FullName, request.Badge, request.Password);
                newUser.SetProject(request.ProjectId);


                await _userRepository.AddAsync(newUser);
                await _userRepository.CommitAsync();

                if (request.RoleIds != null && request.RoleIds.Any())
                {
                    foreach (var roleId in request.RoleIds)
                    {
                        var role = await _roleRepository.GetAsync(r => r.Id == roleId);
                        if (role != null)
                        {
                            var appUserRole = new AppUserRole
                            {
                                AppUserId = newUser.Id,
                                RoleId = roleId
                            };
                            await _userRoleRepository.AddAsync(appUserRole);
                        }
                    }
                    await _userRoleRepository.CommitAsync();
                }

                if (request.NominationIds != null && request.NominationIds.Any())
                {
                    foreach (var nominationId in request.NominationIds)
                    {
                        var nomination = await _nominationRepository.GetAsync(r => r.Id == nominationId);
                        if (nomination != null)
                        {
                            var appUserNomination = new AppUserNomination
                            {
                                AppUserId = newUser.Id,
                                NominationId = nominationId
                            };
                            await _appUserNominationRepository.AddAsync(appUserNomination);
                        }
                    }
                    await _appUserNominationRepository.CommitAsync();
                }

               
                response.IsSuccess = true;
                response.Message = "User created successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception
                response.IsSuccess = false;
                response.Message = "An error occurred while creating the user.";
                // You can log the exception here
            }

            return response;
        }
    }
}
