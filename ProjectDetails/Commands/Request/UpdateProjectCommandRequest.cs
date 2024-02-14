using MediatR;
using ProjectDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Commands.Request;

public class UpdateProjectCommandRequest : IRequest<UpdateProjectCommandResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
