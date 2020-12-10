using Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);

        public void RevokeRefreshToken(User user);
    }
}
