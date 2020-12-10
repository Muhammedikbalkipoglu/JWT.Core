using Api.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IUserService
    {
        UserResponse AddUser(User user);
        UserResponse FindById(int userId);
        UserResponse FindEmailAndPassword(string email, string password);
        void SaveRefreshToken(int userId, string refreshToken);
        UserResponse GetUserWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);
    }
}
