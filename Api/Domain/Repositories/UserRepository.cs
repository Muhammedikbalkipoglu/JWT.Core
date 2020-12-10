using Api.Security.Token;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions _tokenOptions;
        public UserRepository(apitokendbContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            _tokenOptions = tokenOptions.Value;
        }


        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User FindByEmailandPassword(string email, string password)
        {
            return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public User FindById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserWithRefreshToken(string refreshToken)
        {
            return _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public void RemoveRefreshToken(User user)
        {
            User newuser = this.FindById(user.Id);
            newuser.RefreshToken = null;
            newuser.RefreshTokenEndDate = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            User newUser = this.FindById(userId);

            newUser.RefreshToken = refreshToken;
            newUser.RefreshTokenEndDate = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
        }
    }
}
