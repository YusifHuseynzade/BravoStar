using AppUserNominationDetails.Queries.Request;
using AppUserNominationDetails.Queries.Response;
using AutoMapper;
using Domain.IRepositories;
using Infrastructure.Constants;
using MediatR;

namespace AppUserNominationDetails.Handlers.QueryHandlers;

//public class GetAllAppUserVotingQueryHandler : IRequestHandler<GetAllAppUserVotingQueryRequest, List<GetAppUserVotingListResponse>>
//{
//    private readonly IProjectRepository _repository;
//    private readonly IMapper _mapper;

//    public GetAllAppUserVotingQueryHandler(IProjectRepository repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    public async Task<List<GetAppUserVotingListResponse>> Handle(GetAllAppUserVotingQueryRequest request, CancellationToken cancellationToken)
//    {
//        var Projects = _repository.GetAll(x => true);

//        var response = _mapper.Map<List<GetAllAppUserVotingQueryResponse>>(Projects);

//        if (request.ShowMore != null)
//        {
//            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
//        }

//        var totalCount = Projects.Count();

//        PaginationListDto<GetAllAppUserVotingQueryResponse> model =
//               new PaginationListDto<GetAllAppUserVotingQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

//        return new List<GetAppUserVotingListResponse>
//        {
//           new GetAppUserVotingListResponse
//           {
//              TotalAppUserVotingCount = totalCount,
//              AppUserVotes = model.Items
//           }
//        };

//    }
//}
