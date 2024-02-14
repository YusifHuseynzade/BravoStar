using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Queries.Response;
using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApplicationUserDetails.Profiles
{
    public class AppUserMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public AppUserMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            CreateMap<AppUser, GetAllAppUserQueryResponse>();
            CreateMap<AppUser, GetByIdAdminUserQueryResponse>();
            CreateMap<AppUser, GetByIdAppUserQueryResponse>();
            CreateMap<CreateAppUserCommandRequest, AppUser>();
            CreateMap<UpdateAppUserCommandRequest, AppUser>();



            CreateMap<AppUserRole, GetAllAppUserRoleQueryResponse>();
            CreateMap<AppUserRole, GetByIdAppUserRoleQueryResponse>();
            CreateMap<CreateAppUserRoleCommandRequest, AppUserRole>();
            CreateMap<UpdateAppUserRoleCommandRequest, AppUserRole>();
        }
    }
}