using System;

namespace AuthorizationMS.DTO
{
    public class UpdateDTO
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string GuardianType { get; set; }
        public string GuardianName { get; set; }
        public string Address { get; set; }
        public string Citizenship { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public long ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AccountType { get; set; }
        public string BranchName { get; set; }
        public string CitizenStatus { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationDocumentNumber { get; set; }
        public string ReferenceAccountHolderName { get; set; }
        public long ReferenceAccountHolderAccountNumber { get; set; }
        public string ReferenceAccountHolderAddress { get; set; }
    }
}
