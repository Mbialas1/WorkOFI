using Core.Application.Dtos;
using Core.Application.Queries;
using Core.Application.Queries.Users;
using Core.Dtos;
using Core.Entities.User;
using Core.Enums;
using Core.InterfaceRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OFI.UserService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UsersController> logger;
        public UsersController(IUserRepository _userRepository, IMediator _mediator, ILogger<UsersController> _logger)
        {
            userRepository = _userRepository;
            this.mediator = _mediator;
            this.logger = _logger;
        }

        /// <summary>
        /// Task for test.
        /// </summary>
        [HttpGet("users/allUsers")]
        public async Task<ActionResult<IEnumerable<UserDashboardDTO>>> GetAllUsers()
        {
            logger.LogInformation($"{nameof(GetAllUsers)} function just started");
            try
            {
                var query = new GetAllUsersForDashboardQueries();
                var users = await mediator.Send(query);

                if (users == null || !users.Any())
                {
                    return NotFound("No users found.");
                }

                logger.LogInformation($"{nameof(GetAllUsers)} -- Users download");
                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(GetAllUsers)} with detail error : {ex.Message} ");
                return BadRequest("All users can't be donwload");
            }
        }
    }
}
