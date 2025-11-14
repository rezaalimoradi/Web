using Application.Features.Request.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetUsersQuery());

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
