using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Commands.Request;
using ProjectDetails.Queries.Request;

namespace BravoStar.AdminPanel.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ProjectController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] GetAllProjectQueryRequest request)
		{
			var projects = await _mediator.Send(request);

			return Ok(projects);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var requestModel = new GetByIdProjectQueryRequest { Id = id };
			var Project = await _mediator.Send(requestModel);

			return Project != null
				? (IActionResult)Ok(Project)
				: NotFound(new { Message = "Project not found." });
		}

	}
}
