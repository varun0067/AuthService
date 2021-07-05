using AuthorizationMS.DTO;
using AuthorizationMS.Interface;
using AuthorizationMS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMS.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration,IAccountRepository repository)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<Account> GetUserById(string customerId)
        {
            return await _repository.GetUserById(customerId);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _repository.GetAccounts();
        }

        public async Task<List<string>> GetUsernames()
        {
            return await _repository.GetUsernames();
        }

        public async Task RegisterUser(Account account)
        {
           await _repository.RegisterUser(account);
        }

        public async Task<bool> LoginCheck(UserDTO user)
        {
            Account account = await _repository.GetUserByUsername(user.Username);

            if (account != null && account.Password == user.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<string> UpdateUser(UpdateDTO account)
        {
            Account user = await _repository.GetUserById(account.CustomerId);

            if (user != null)
            {
                bool updated = await _repository.UpdateUser(account, user.CustomerId);
                if (updated)
                    return "details Updated successfully";
                else 
                    return "could'nt update details some error occured";
            }
            else
                return "user does'nt exists";
        }

        public async Task<UserTokenDTO> CreateJwt(UserDTO user)
        {
            Account account =await _repository.GetUserByUsername(user.Username);

            string Key = _configuration["Token:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string userRole = "user";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim("CustomerId", account.CustomerId.ToString())
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

            UserTokenDTO tokenUser = new UserTokenDTO
            {
                CustomerId = account.CustomerId,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return tokenUser;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordDTO forgotPassword)
        {
            Account user =await  _repository.GetUserByUsername(forgotPassword.Username);

            if (user != null && forgotPassword.DateOfBirth == user.DateOfBirth)
            {
                bool changePassword = await _repository.UpdatePassword(user.Username, forgotPassword.Password);
                return changePassword;

            }
            else
                return false;
        }

        public async Task<bool> ChangePassword(ChangePasswordDTO changePassword)
        {
            Account user =await _repository.GetUserById(changePassword.CustomerId);

            if (user != null && changePassword.OldPassword == user.Password)
            {
                bool changePass =await _repository.UpdatePassword(user.Username, changePassword.Password);

                return changePass;
            }
            else
                return false;
        }
    }
}
