using MediatR;
using ProjectDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Request;

public class GetByIdProjectQueryRequest : IRequest<GetByIdProjectQueryResponse>
{
    public int Id { get; set; }
}
