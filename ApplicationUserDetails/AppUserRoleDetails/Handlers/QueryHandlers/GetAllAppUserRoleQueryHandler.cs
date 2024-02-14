using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using ApplicationUserDetails.AppUserRoleDetails.Queries.Response;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.QueryHandlers
{
    public class GetAllAppUserRoleQueryHandler : IRequestHandler<GetAllAppUserRoleQueryRequest, List<GetAllAppUserRoleQueryResponse>>
    {
        private readonly IAppUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllAppUserRoleQueryHandler(IAppUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllAppUserRoleQueryResponse>> Handle(GetAllAppUserRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var appuserroles = _repository.GetAll(x => true);

            if (appuserroles != null)
            {
                List<GetAllAppUserRoleQueryResponse> response = _mapper.Map<List<GetAllAppUserRoleQueryResponse>>(appuserroles);
                return response;
            }
            else
            {
                return new List<GetAllAppUserRoleQueryResponse>();
            }
        }
    }
}