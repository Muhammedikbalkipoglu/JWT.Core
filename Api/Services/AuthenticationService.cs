using Api.Domain.Responses;
using Api.Domain.Services;
using Api.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }
        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            UserResponse userResponse = _userService.FindEmailAndPassword(email, password);
            if (userResponse.Success)
            {
                AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.user);

                _userService.SaveRefreshToken(userResponse.user.Id, accessToken.RefreshToken);

                return new AccessTokenResponse(accessToken);
            }
            else
            {
                return new AccessTokenResponse(userResponse.Message);
            }
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                if (userResponse.user.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = _tokenHandler.CreateAccessToken(userResponse.user);

                    _userService.SaveRefreshToken(userResponse.user.Id, accessToken.RefreshToken);

                    return new AccessTokenResponse(accessToken);
                }
                else
                {
                    return new AccessTokenResponse("refreshtoken time is finish");
                    
                }
            }
            else
            {
                return new AccessTokenResponse("refreshtoken is not found");
            }
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse = _userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                _userService.RemoveRefreshToken(userResponse.user);
                return new AccessTokenResponse(new AccessToken());
            }
            else
            {
                return new AccessTokenResponse("refreshtoken is not found");
            }
        }
    }
}
