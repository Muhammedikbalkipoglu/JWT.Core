using Api.Domain.Services;
using Api.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Domain.Responses;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Accesstoken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                AccessTokenResponse accessTokenResponse = _authenticationService.
                    CreateAccessToken(loginResource.Email, 
                    loginResource.Password);
                
                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.accessToken);
                }
                else
                {
                    return BadRequest(accessTokenResponse.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            AccessTokenResponse accessTokenResponse = _authenticationService.
                CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);

            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }

        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource )
        {
            AccessTokenResponse accessTokenResponse = _authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);
            
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
            
        }
    }
}
