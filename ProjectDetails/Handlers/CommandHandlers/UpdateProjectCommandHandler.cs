using Domain.IRepositories;
using MediatR;
using ProjectDetails.Commands.Request;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Handlers.CommandHandlers;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommandRequest, UpdateProjectCommandResponse>
{

    private readonly IProjectRepository _repository;

	public UpdateProjectCommandHandler(IProjectRepository repository)
	{
		_repository = repository;
	}

	public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        var existingProject = await _repository.GetAsync(d => d.Id == request.Id);

        // Əgər Project tapılmazsa false qaytar
        if (existingProject == null)
        {
            return new UpdateProjectCommandResponse
            {
                IsSuccess = false
            };
        }

        // Yeni adı istifadə olunan adla müqayisə etmək və eyni adlı Project olmamasına əmin olmaq
        if (await _repository.IsExistAsync(d => d.Name == request.Name && d.Id != request.Id))
        {
            return new UpdateProjectCommandResponse
            {
                IsSuccess = false
            };
        }
		existingProject.SetDetail(request.Name);
        await _repository.UpdateAsync(existingProject);
        await _repository.CommitAsync();

        return new UpdateProjectCommandResponse
        {
            IsSuccess = true
        };
    }
}
