using AuthorizationMS.DTO;
using AuthorizationMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorizationMS.Interface
{
    public interface IAccountRepository
    {
        public Task<Account> GetUserByUsername(string username);
        public Task<List<string>> GetUsernames();
        public Task<Account> GetUserById(string customerId);
        public Task RegisterUser(Account account);
        public Task<bool> UpdateUser(UpdateDTO account,string customerId);
        public Task<bool> UpdatePassword(string username, string password);
        public Task<List<Account>> GetAccounts();
    }
}
