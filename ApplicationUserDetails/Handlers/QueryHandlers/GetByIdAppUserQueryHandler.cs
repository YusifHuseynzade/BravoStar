using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationUserDetails.Handlers.QueryHandlers
{
    public class GetByIdAppUserQueryHandler : IRequestHandler<GetByIdAppUserQueryRequest, GetByIdAppUserQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetByIdAppUserQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetByIdAppUserQueryResponse> Handle(GetByIdAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            if (_context != null)
            {
                var user = await _context.AppUsers.FirstOrDefaultAsync(p => p.Id == request.Id);

                if (user != null)
                {
                    var response = _mapper.Map<GetByIdAppUserQueryResponse>(user);
                    return response;
                }
            }

            return new GetByIdAppUserQueryResponse();
        }
    }
}
