using Api.Domain;
using Api.Domain.Responses;
using Api.Domain.Services;
using Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            UserResponse userResponse = _userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = _mapper.Map<UserResource, User>(userResource);

            UserResponse userResponse = _userService.AddUser(user);

            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }

            else
            {
                return BadRequest(userResponse.Message);
            }
        }
    }
}
