﻿using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        /// <summary></summary>
        /// <param name="userRepository"></param>
        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepository.Exists(x=>x.Email == email);
        }

        public bool IsPhoneNumberExist(string phoneNumber)
        {
            return _userRepository.Exists(x => x.PhoneNumber == phoneNumber);
        }
    }
}
