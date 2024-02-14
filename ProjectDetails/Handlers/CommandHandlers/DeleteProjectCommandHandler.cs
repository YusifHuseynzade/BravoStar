using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Handlers.CommandHandlers;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommandRequest, DeleteProjectCommandResponse>
{
    private readonly IProjectRepository _repository;

    public DeleteProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommandRequest request, CancellationToken cancellationToken)
    {
        var Project = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Project == null)
        {
            return new DeleteProjectCommandResponse { IsSuccess = false };
        }

        _repository.Remove(Project);
        await _repository.CommitAsync();

        return new DeleteProjectCommandResponse
        {
            IsSuccess = true
        };
    }
}
