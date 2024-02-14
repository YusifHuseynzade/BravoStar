using AutoMapper;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Queries.Request;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Handlers.QueryHandlers;

public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQueryRequest, GetByIdProjectQueryResponse>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdProjectQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdProjectQueryResponse> Handle(GetByIdProjectQueryRequest request, CancellationToken cancellationToken)
    {
        var Projects = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Projects != null)
        {
            var response = _mapper.Map<GetByIdProjectQueryResponse>(Projects);
            return response;
        }

        return new GetByIdProjectQueryResponse();

    }
}