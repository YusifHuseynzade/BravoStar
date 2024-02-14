using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AppUserNominationDetails.Profiles;

public class AppUserNominationMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public AppUserNominationMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}

