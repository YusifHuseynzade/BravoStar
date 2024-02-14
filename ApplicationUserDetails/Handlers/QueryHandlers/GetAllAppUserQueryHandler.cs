using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationUserDetails.Handlers.QueryHandlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllAppUserQueryRequest, List<GetAppUserListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _repository;

        public GetAllUserQueryHandler(IMapper mapper, IAppUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<GetAppUserListResponse>> Handle(GetAllAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            var appUsers = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllAppUserQueryResponse>>(appUsers);

            // Show more functionality
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take)
                                   .Take(request.ShowMore.Take)
                                   .ToList();
            }

            var totalCount = appUsers.Count();

            PaginationListDto<GetAllAppUserQueryResponse> model =
              new PaginationListDto<GetAllAppUserQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetAppUserListResponse>
            {
                new GetAppUserListResponse
                {
                    TotalAppUserCount = totalCount,
                    AppUsers = response
                }
            };
        }
    }
}
