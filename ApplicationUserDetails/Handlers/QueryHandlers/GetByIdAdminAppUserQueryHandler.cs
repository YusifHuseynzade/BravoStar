using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationUserDetails.Handlers.QueryHandlers
{
    public class GetByIdAdminUserQueryHandler : IRequestHandler<GetByIdAdminUserQueryRequest, GetByIdAdminUserQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetByIdAdminUserQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetByIdAdminUserQueryResponse> Handle(GetByIdAdminUserQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context != null)
            {
                var user = await _context.AppUsers.Include(u => u.Project).FirstOrDefaultAsync(p => p.Id == request.Id);

                if (user != null)
                {
                    var response = _mapper.Map<GetByIdAdminUserQueryResponse>(user);
                    return response;
                }
            }

            return new GetByIdAdminUserQueryResponse();
        }
    }
}
