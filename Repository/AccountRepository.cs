using AuthorizationMS.DTO;
using AuthorizationMS.Interface;
using AuthorizationMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(Account account)
        {

            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();
        }

        public async Task<Account> GetUserByUsername(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account => account.Username == username);
        }

        public async Task<bool> UpdatePassword(string username, string password)
        {
            var user =await _context.Accounts.FirstOrDefaultAsync(account => account.Username == username);
            if (user != null)
            {
                user.Password = password;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> UpdateUser(UpdateDTO account,string customerId)
        {
            var user =await _context.Accounts.FirstOrDefaultAsync(account => account.CustomerId == customerId);
            try
            {
                if (user != null)
                {
                    user.Name = account.Name;
                    user.Username = account.Username;
                    user.GuardianType = account.GuardianType;
                    user.GuardianName = account.GuardianName;
                    user.Address = account.Address;
                    user.Citizenship = account.Citizenship;
                    user.State = account.State;
                    user.Country = account.Country;
                    user.Email = account.Email;
                    user.Gender = account.Gender;
                    user.MaritalStatus = account.MaritalStatus;
                    user.ContactNumber = account.ContactNumber;
                    user.DateOfBirth = account.DateOfBirth;
                    user.AccountType = account.AccountType;
                    user.BranchName = account.BranchName;
                    user.Citizenship = account.Citizenship;
                    user.IdentificationType = account.IdentificationType;
                    user.IdentificationDocumentNumber = account.IdentificationDocumentNumber;
                    user.ReferenceAccountHolderName = account.ReferenceAccountHolderName;
                    user.ReferenceAccountHolderAccountNumber = account.ReferenceAccountHolderAccountNumber;
                    user.ReferenceAccountHolderAddress = account.ReferenceAccountHolderAddress;
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<Account> GetUserById(string customerId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account => account.CustomerId == customerId);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<List<string>> GetUsernames()
        {
            return  await _context.Accounts.Select(x => x.Username).ToListAsync();
        }
    }
}
