using MediatR;
using ProjectDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Commands.Request;

public class DeleteProjectCommandRequest : IRequest<DeleteProjectCommandResponse>
{
    public int Id { get; set; }
}
