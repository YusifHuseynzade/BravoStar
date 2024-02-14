using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using ApplicationUserDetails.AppUserRoleDetails.Queries.Response;
using AutoMapper;
using Domain.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.QueryHandlers
{
    public class GetByIdAppUserRoleQueryHandler : IRequestHandler<GetByIdAppUserRoleQueryRequest, GetByIdAppUserRoleQueryResponse>
    {
        private readonly IAppUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdAppUserRoleQueryHandler(IAppUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetByIdAppUserRoleQueryResponse> Handle(GetByIdAppUserRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var appuserrole = await _repository.FirstOrDefaultAsync(p => p.Id == request.Id);

            if (appuserrole != null)
            {
                var response = _mapper.Map<GetByIdAppUserRoleQueryResponse>(appuserrole);
                return response;
            }

            return new GetByIdAppUserRoleQueryResponse();
        }
    }
}