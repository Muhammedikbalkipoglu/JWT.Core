using Api.Domain;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using Api.Domain.Services;
using Api.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public UserResponse AddUser(User user)
        {
            try
            {
                _userRepository.AddUser(user);
                _unitOfWork.Complate();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"I have an exception While add user:{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                User user = _userRepository.FindById(userId);

                if (user == null)
                {
                    return new UserResponse("Can not find user");
                }

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"I have an exception While find user:{ex.Message}");
            }
        }

        public UserResponse FindEmailAndPassword(string email, string password)
        {
            try
            {
                User user = _userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new UserResponse("Can not found user.");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"I have an exception while find Useremalil and Userpassword:{ex.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                User user = _userRepository.GetUserWithRefreshToken(refreshToken);

                if (user == null)
                {
                    return new UserResponse("Can not Found user.");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"I have an exception Wwhile find refreshtoken:{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                _userRepository.RemoveRefreshToken(user);
                _unitOfWork.Complate();
            }
            catch (Exception)
            {
                //loglama yapılabilir

            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                _userRepository.SaveRefreshToken(userId, refreshToken);
                _unitOfWork.Complate();
            }
            catch (Exception)
            {

                //loglama yapılabilir
            }

        }
    }
}
