using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Handlers.CommandHandlers;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    private readonly IProjectRepository _repository;
   

    public CreateProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(d => d.Name == request.Name))
        {
            return new CreateProjectCommandResponse
            {
                IsSuccess = false,
            };
        }

        var project = new Project();
        project.SetDetail(request.Name);

        await _repository.AddAsync(project);
        await _repository.CommitAsync();

        return new CreateProjectCommandResponse
        {
            IsSuccess = true
        };
    }
}
