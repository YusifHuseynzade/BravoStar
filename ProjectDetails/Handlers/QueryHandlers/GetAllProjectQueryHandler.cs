using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Constants;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Handlers.QueryHandlers;

public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQueryRequest, List<GetProjectListResponse>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProjectQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetProjectListResponse>> Handle(GetAllProjectQueryRequest request, CancellationToken cancellationToken)
    {
        var Projects = _repository.GetAll(x => true);
       
        var response = _mapper.Map<List<GetAllProjectQueryResponse>>(Projects);
       
        if (request.ShowMore != null)
        {
            response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        var totalCount = Projects.Count();

        PaginationListDto<GetAllProjectQueryResponse> model =
               new PaginationListDto<GetAllProjectQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

        return new List<GetProjectListResponse>
        {
           new GetProjectListResponse
           {
              TotalProjectCount = totalCount,
              Projects = model.Items
           }
        };

    }
}
