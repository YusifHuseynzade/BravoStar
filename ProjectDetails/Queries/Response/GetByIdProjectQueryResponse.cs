﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDetails.Queries.Response;

public class GetByIdProjectQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
}
