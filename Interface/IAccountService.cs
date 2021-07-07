using AuthorizationMS.DTO;
using AuthorizationMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationMS.Interface
{
    public interface IAccountService
    {
        public List<Account> GetAccounts();
        public Task<List<string>> GetUsernames();
        public Task<Account> GetUserById(string customerId);
        public Task RegisterUser(Account account);
        public Task<bool> LoginCheck(UserDTO user);
        public Task<string> UpdateUser(UpdateDTO account);
        public Task<bool> ForgotPassword(ForgotPasswordDTO forgotPassword);
        public Task<bool> ChangePassword(ChangePasswordDTO changePassword);
        public Task<UserTokenDTO> CreateJwt(UserDTO user);
    }
}
