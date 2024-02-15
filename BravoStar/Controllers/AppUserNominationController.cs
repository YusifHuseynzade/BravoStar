using AppUserNominationDetails.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BravoStar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserNominationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserNominationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUserVoting([FromQuery] CreateAppUserVotingCommandRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Gerekirse hata durumunda uygun bir cevap döndürülebilir
                return StatusCode(500, new { message = "İşlem sırasında bir hata oluştu.", error = ex.Message });
            }
        }

    }
}
