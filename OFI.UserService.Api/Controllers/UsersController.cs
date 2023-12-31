﻿using Core.Application.Commands.Users;
using Core.Application.Dtos;
using Core.Application.Queries;
using Core.Application.Queries.Users;
using Core.Authorization.Dtos;
using Core.Dtos;
using Core.Entities.User;
using Core.Enums;
using Core.InterfaceRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OFI.Infrastructure.User.Handlers.Users.Commands;
using System.Text.RegularExpressions;
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

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginComponentDTO loginComponentDTO)
        {
            logger.LogInformation($"{nameof(Login)} function just started");
            try
            {
                if (String.IsNullOrEmpty(loginComponentDTO.Login) || String.IsNullOrEmpty(loginComponentDTO.Password))
                {
                    logger.LogError($"Error in {nameof(Login)} login or password is empty.");
                    return Unauthorized("Bad login or password");
                }

                var command = new LoginToApplicationCommand(loginComponentDTO);
                var result = await mediator.Send(command); //TODO Change to token name

                if(result is null)
                {
                    return Unauthorized("Bad login or password!");
                }

                return Ok(); //TODO Return JWT hear
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(Login)} with detail error : {ex.Message} ");
                return Unauthorized();
            }
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterLoginComponentDTO registerLoginComponentDTO)
        {
            logger.LogInformation($"{nameof(RegisterUser)} function just started");
            try
            {
                var command = new RegisterToApplicationCommand(registerLoginComponentDTO);
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(RegisterUser)} with detail error : {ex.Message} ");
                return Unauthorized();
            }
        }

        [HttpGet("users/search")]
        public async Task<ActionResult<IEnumerable<UserDashboardDTO>>> SearchUserForFiltr([FromQuery] string characters)
        {
            logger.LogInformation($"{nameof(SearchUserForFiltr)} function just started");
            try
            {
                if (string.IsNullOrEmpty(characters))
                {
                    logger.LogError("Characters is null!");
                    return BadRequest("Character is null");
                }
                else
                {
                    if (!Regex.IsMatch(characters, @"^[a-zA-Z]+$"))
                    {
                        logger.LogError($"{nameof(SearchUserForFiltr)} only letters in charcters");
                        return BadRequest("Only letters please in characters");
                    }

                    var query = new GetUsersByFiltrQuery(characters);
                    var users = await mediator.Send(query);

                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(SearchUserForFiltr)} with detail error : {ex.Message} ");
                return BadRequest();
            }
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long userId)
        {
            logger.LogInformation($"{nameof(GetUserById)} function just started");
            try
            {
                var query = new GetUserByIdQueries(userId);
                var user = await mediator.Send(query);

                if (user == null)
                {
                    return NotFound("No users found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(GetUserById)} with detail error : {ex.Message} ");
                return BadRequest("Cant get user by this id");
            }
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
