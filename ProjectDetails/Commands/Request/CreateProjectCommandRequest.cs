using MediatR;
using ProjectDetails.Commands.Response;

namespace ProjectDetails.Commands.Request;

public class CreateProjectCommandRequest : IRequest<CreateProjectCommandResponse>
{
    public string Name { get; set; }
}
